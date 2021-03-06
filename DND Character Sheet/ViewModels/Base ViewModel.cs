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

        public BaseViewModel(ICharacterModel character, IDialogWindowWrapper dialogWindowWrapper, IStaticClassWrapper staticClassWrapper, IOpenNewViewWrapper windowServiceWrapper)
        {
            InitialiseCharacter(character);
            InitialiseDialogWindowWrapper(dialogWindowWrapper);
            InitialiseStaticClassWrapper(staticClassWrapper);
            InitialiseWindowWrapper(windowServiceWrapper);
        }

        #region Initialise

        private void InitialiseStaticClassWrapper(IStaticClassWrapper staticClassWrapper) 
            => StaticClassWrapper = staticClassWrapper;

        private void InitialiseWindowWrapper(IOpenNewViewWrapper windowServiceWrapper) 
            => WindowServiceWrapper = windowServiceWrapper;

        protected void InitialiseCharacter(ICharacterModel character)
            => Character = character;

        protected void InitialiseDialogWindowWrapper(IDialogWindowWrapper dialogWindowWrapper) 
            => DialogWindowWrapper = dialogWindowWrapper;

        protected void InitialiseWindowTitle()
            => WindowTitle = Character.FilePath == Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                ? "Test"
                : StaticClassWrapper.TextFormatterWrapper.ExtractFileNameFromPath(Character.FilePath);

        #endregion

        public bool SaveCharacterAs()
        {
            DialogWindowWrapper.SaveFileDialogWrapper.SaveFileDialog.Filter = "DND Characters|*.json|All files|*.*";
            DialogWindowWrapper.SaveFileDialogWrapper.SaveFileDialog.InitialDirectory = Character.FilePath;
            DialogWindowWrapper.SaveFileDialogWrapper.SaveFileDialog.FileName = "";

            if (DialogWindowWrapper.SaveFileDialogWrapper.ShowDialog())
            {
                Character.FilePath = DialogWindowWrapper.SaveFileDialogWrapper.SaveFileDialog.FileName;
                Save();
                return true;
            }

            return false;
        }

        public bool SaveCharacter()
        {
            if (Character.FilePath == Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments))
                return SaveCharacterAs();

            Save();

            return true;
        }

        public bool OpenCharacter()
        {
            DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.Filter = "DND Characters|*.json|All files|*.*";
            DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.InitialDirectory = Character.FilePath;
            DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.FileName = "";

            if (DialogWindowWrapper.OpenFileDialogWrapper.ShowDialog())
            {
                if (!CheckSubMenus())
                {
                    WindowServiceWrapper.CloseAllSubWindows();
                    Character = Open(DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.FileName);
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
                    if (HasCharacterBeenCreated())
                    {
                        Save();
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
                if (!(new Comparer<ICharacterModel>().Compare(Open(Character.FilePath), Character, out var dif)))
                    return SaveChangesMessageBox;
                return MessageBoxResult.No;
            }

            if (!(new Comparer<ICharacterModel>().Compare(Character, new CharacterModel())))
                return SaveChangesMessageBox;

            return MessageBoxResult.No;
        }

        private MessageBoxResult SaveChangesMessageBox 
            => DialogWindowWrapper.MessageBoxWrapper.Show("Would you like to save your changes before closing?", "Close Character", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.Yes);

        private void Save()
            => StaticClassWrapper.SerializeCharacterWrapper.SaveCharacterToFile(Character);

        private ICharacterModel Open(string filePath)
            => StaticClassWrapper.SerializeCharacterWrapper.OpenCharacterFromFile(filePath);

        private bool HasCharacterBeenCreated() 
            => Character.FilePath != Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
