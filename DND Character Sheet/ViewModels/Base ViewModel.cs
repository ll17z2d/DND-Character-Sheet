using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using DND_Character_Sheet.Annotations;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.Serialization;
using DND_Character_Sheet.Views;
using DND_Character_Sheet.Wrappers;
using ObjectsComparer;

namespace DND_Character_Sheet.ViewModels
{
    public interface IBaseViewModel
    {
        public ICharacterModel Character { get; set; }

        public ICommand SaveCharacterCommand { get; set; }

        public IDialogWindowWrapper DialogWindowWrapper { get; set; }
        
        public IStaticClassWrapper StaticClassWrapper { get; set; }

        public IOpenNewViewWrapper WindowServiceWrapper { get; set; }

        public bool SaveCharacterAs();

        public bool SaveCharacter();

        public bool OpenCharacter();

        public void ExitWindow(object sender, CancelEventArgs e);

        public MessageBoxResult CanExitWindow();
    }

    public abstract class BaseViewModel : IBaseViewModel, INotifyPropertyChanged
    {
        private ICharacterModel character;

        public ICharacterModel Character
        {
            get
            {
                return character;
            }
            set
            {
                character = value;
                OnPropertyChanged("Character");
            }
        }

        private string windowTitle;

        public string WindowTitle
        {
            get
            {
                return windowTitle;
            }
            set
            {
                windowTitle = value;
                OnPropertyChanged("WindowTitle");
            }
        }

        public ICommand SaveCharacterCommand { get; set; }

        public IDialogWindowWrapper DialogWindowWrapper { get; set; }

        public IStaticClassWrapper StaticClassWrapper { get; set; }

        public IOpenNewViewWrapper WindowServiceWrapper { get; set; }

        public IFileOperationsWrapper FileOperationsWrapper { get; set; }

        public BaseViewModel(ICharacterModel character, IDialogWindowWrapper dialogWindowWrapper, IStaticClassWrapper staticClassWrapper, 
            IOpenNewViewWrapper windowServiceWrapper)
        {
            InitialiseCharacter(character);
            InitialiseDialogWindowWrapper(dialogWindowWrapper);
            InitialiseStaticClassWrapper(staticClassWrapper);
            InitialiseWindowWrapper(windowServiceWrapper);
        }

        #region Initialise

        protected void InitialiseCharacter(ICharacterModel character)
            => Character = character;

        private void InitialiseStaticClassWrapper(IStaticClassWrapper staticClassWrapper) 
            => StaticClassWrapper = staticClassWrapper;

        private void InitialiseWindowWrapper(IOpenNewViewWrapper windowServiceWrapper) 
            => WindowServiceWrapper = windowServiceWrapper;

        private void InitialiseDialogWindowWrapper(IDialogWindowWrapper dialogWindowWrapper) 
            => DialogWindowWrapper = dialogWindowWrapper;

        protected void InitialiseWindowTitle()
            => WindowTitle = Character.FilePath == Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                ? "Test"
                : StaticClassWrapper.TextFormatterWrapper.ExtractFileNameFromPath(Character.FilePath);

        #endregion

        public bool SaveCharacterAs()
        {
            DialogWindowWrapper.SaveFileDialogWrapper.SaveFileDialog.Filter = DialogWindowWrapper.FileDialogFilter;
            DialogWindowWrapper.SaveFileDialogWrapper.SaveFileDialog.InitialDirectory = Character.FilePath;
            DialogWindowWrapper.SaveFileDialogWrapper.FileName = "";

            if (DialogWindowWrapper.SaveFileDialogWrapper.ShowDialog())
            {
                if (DialogWindowWrapper.SaveFileDialogWrapper.FilterIndex == 1) 
                {
                    Character.FilePath = DialogWindowWrapper.SaveFileDialogWrapper.FileName;
                    SaveJSON();
                    return true; 
                }
                else if (DialogWindowWrapper.SaveFileDialogWrapper.FilterIndex == 2)
                {
                    Character.FilePath = DialogWindowWrapper.SaveFileDialogWrapper.FileName;
                    SavePDF();
                    return true;
                }
            }

            return false;
        }

        public bool SaveCharacter()
        {
            if (Character.FilePath == Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments))
                return SaveCharacterAs();

            SaveJSON();

            return true;
        }

        public bool OpenCharacter()
        {
            DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.Filter = DialogWindowWrapper.FileDialogFilter;
            DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.InitialDirectory = Character.FilePath;
            DialogWindowWrapper.OpenFileDialogWrapper.FileName = "";

            if (DialogWindowWrapper.OpenFileDialogWrapper.ShowDialog())
            {
                if (!CheckSubMenus())
                {
                    WindowServiceWrapper.CloseAllSubWindows();
                    if (DialogWindowWrapper.OpenFileDialogWrapper.FilterIndex == 1)
                        Character = OpenJSON(DialogWindowWrapper.OpenFileDialogWrapper.FileName);
                    else if (DialogWindowWrapper.OpenFileDialogWrapper.FilterIndex == 2)
                        Character = OpenPDF(DialogWindowWrapper.OpenFileDialogWrapper.FileName);
                    InitialiseWindowTitle();
                    return true;
                }

                return false;
            }

            return false;
        }

        private bool CheckSubMenus()
        {
            var cancelEventArgs = new CancelEventArgs();
            ExitWindow(new object(), cancelEventArgs);
            return cancelEventArgs.Cancel;
        }

        public void ExitWindow(object sender, CancelEventArgs e)
        {
            var result = CanExitWindow();
            switch (result)
            {
                case MessageBoxResult.Yes:
                    WindowServiceWrapper.CloseAllSubWindows();
                    if (StaticClassWrapper.TextFormatterWrapper.IsJSON(Character.FilePath) && HasCharacterBeenCreated())
                    {
                        SaveJSON();
                        e.Cancel = false;
                    }
                    else
                        e.Cancel = !SaveCharacterAs();
                    return;
                case MessageBoxResult.No:
                    WindowServiceWrapper.CloseAllSubWindows();
                    e.Cancel = false;
                    break;
                case MessageBoxResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }

        public MessageBoxResult CanExitWindow()
        {
            //The below check is needed to make sure we don't try to save when the user hasn't actually saved their character to the computer yet
            if (HasCharacterBeenCreated())
            {
                if (!StaticClassWrapper.TextFormatterWrapper.IsJSON(Character.FilePath) || !(new Comparer<ICharacterModel>().Compare(OpenJSON(Character.FilePath), Character, out var dif))) //Look into this being compatible with pdf too
                    return SaveChangesMessageBox;
                return MessageBoxResult.No;
            }

            if (!(new Comparer<ICharacterModel>().Compare(Character, new CharacterModel())))
                return SaveChangesMessageBox;

            return MessageBoxResult.No;
        }

        private MessageBoxResult SaveChangesMessageBox 
            => DialogWindowWrapper.MessageBoxWrapper.Show("Would you like to save your changes before closing?", "Close Character", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.Yes);
        
        private void SaveJSON()
            => StaticClassWrapper.SerializeCharacterWrapper.SaveCharacterToFileJSON(Character);

        private void SavePDF()
            => StaticClassWrapper.SerializeCharacterWrapper.SaveCharacterToFilePDF(Character);

        private ICharacterModel OpenJSON(string filePath)
            => StaticClassWrapper.SerializeCharacterWrapper.OpenCharacterFromFileJSON(filePath);

        private ICharacterModel OpenPDF(string filePath)
            => StaticClassWrapper.SerializeCharacterWrapper.OpenCharacterFromFileJSON(filePath);

        private bool HasCharacterBeenCreated() 
            => Character.FilePath != Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
