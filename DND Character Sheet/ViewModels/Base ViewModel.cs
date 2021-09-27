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

        public ITextFormatterWrapper TextFormatterWrapper { get; set; }

        public ISerializeCharacterWrapper SerializeCharacterWrapper { get; set; }

        public IWindowServiceWrapper WindowServiceWrapper { get; set; }

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

        public ICommand SaveCharacterCommand { get; set; }

        public virtual IDialogWindowWrapper DialogWindowWrapper { get; set; }

        public ITextFormatterWrapper TextFormatterWrapper { get; set; }

        public ISerializeCharacterWrapper SerializeCharacterWrapper { get; set; }

        public IWindowServiceWrapper WindowServiceWrapper { get; set; }

        public BaseViewModel(ICharacterModel character, IDialogWindowWrapper dialogWindowWrapper, ITextFormatterWrapper textFormatterWrapper, ISerializeCharacterWrapper serializeCharacterWrapper, IWindowServiceWrapper windowServiceWrapper)
        {
            InitialiseCharacter(character);
            InitialiseDialogWindowWrapper(dialogWindowWrapper);
            InitialiseTextFormatterWrapper(textFormatterWrapper);
            InitialiseSerializeCharacterWrapper(serializeCharacterWrapper);
            InitialiseWindowWrapper(windowServiceWrapper);
        }

        #region Initialise

        private void InitialiseWindowWrapper(IWindowServiceWrapper windowServiceWrapper) 
            => WindowServiceWrapper = windowServiceWrapper;

        public void InitialiseCharacter(ICharacterModel character)
            => Character = character;

        public void InitialiseDialogWindowWrapper(IDialogWindowWrapper dialogWindowWrapper) 
            => DialogWindowWrapper = dialogWindowWrapper;

        public void InitialiseTextFormatterWrapper(ITextFormatterWrapper textFormatterWrapper)
            => TextFormatterWrapper = textFormatterWrapper;

        public void InitialiseSerializeCharacterWrapper(ISerializeCharacterWrapper serializeCharacterWrapper)
            => SerializeCharacterWrapper = serializeCharacterWrapper;

        #endregion

        public bool SaveCharacterAs()
        {
            DialogWindowWrapper.SaveFileDialogWrapper.SaveFileDialog.Filter = "DND Characters|*.json|All files|*.*";
            DialogWindowWrapper.SaveFileDialogWrapper.SaveFileDialog.InitialDirectory = Character.FilePath;
            var dialogResult = DialogWindowWrapper.SaveFileDialogWrapper.ShowDialog();

            if (dialogResult == true)
            {
                Character.FilePath = DialogWindowWrapper.SaveFileDialogWrapper.SaveFileDialog.FileName;
                Save();
            }

            return (bool)dialogResult;
        }

        public bool SaveCharacter()
        {
            if (Character.FilePath == Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments))
            {
                return SaveCharacterAs();
            }

            Save();

            return true;
        }

        public bool OpenCharacter()
        {
            DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.Filter = "DND Characters|*.json|All files|*.*";
            DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.InitialDirectory = Character.FilePath;
            var dialogResult = DialogWindowWrapper.OpenFileDialogWrapper.ShowDialog();

            if (dialogResult == true)
            {
                Character = Open(DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.FileName);
            }

            return (bool)dialogResult;
        }

        public void ExitWindow(object sender, CancelEventArgs e)
        {
            var result = CanExitWindow();
            switch (result)
            {
                case MessageBoxResult.Yes:
                    if (HasCharacterBeenCreated())
                        Save();
                    else
                        SaveCharacterAs();
                    e.Cancel = false;
                    return;
                case MessageBoxResult.No:
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
                if (!(new Comparer<ICharacterModel>().Compare(Open(Character.FilePath), Character)))
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
            => SerializeCharacterWrapper.SaveCharacterToFile(Character);

        private ICharacterModel Open(string filePath)
            => SerializeCharacterWrapper.OpenCharacterFromFile(filePath);

        private bool HasCharacterBeenCreated() 
            => Character.FilePath != Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
