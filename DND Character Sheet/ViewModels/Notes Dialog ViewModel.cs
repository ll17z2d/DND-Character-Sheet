using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using DND_Character_Sheet.Annotations;
using DND_Character_Sheet.Commands;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.Wrappers;
using Microsoft.Win32;

namespace DND_Character_Sheet.ViewModels
{
    public class NotesDialogViewModel : INotifyPropertyChanged
    {
        public NotesDialogViewModel(CharacterNotes characterNotes, string characterFilePath, IDialogWindowWrapper dialogWindowWrapper)
        {
            CharacterNotes = characterNotes;
            CharacterFilePath = characterFilePath;
            DialogWindowWrapper = dialogWindowWrapper;
            BrowseCharacterAppearanceCommand = new MethodCommands(BrowseCharacterAppearance);
        }

        public ICommand BrowseCharacterAppearanceCommand { get; set; }

        private CharacterNotes characterNotes;

        public CharacterNotes CharacterNotes
        {
            get
            {
                return characterNotes;
            }
            set
            {
                characterNotes = value;
                OnPropertyChanged("CharacterNotes");
            }
        }

        public string CharacterFilePath { get; set; }

        public IDialogWindowWrapper DialogWindowWrapper { get; set; }

        public bool BrowseCharacterAppearance()
        {
            DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.Filter = "All files|*.*";
            DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.InitialDirectory = CharacterFilePath;

            if (DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.ShowDialog() == true)
                CharacterNotes.CharacterAppearanceFilePath =
                    DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.FileName;

            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
