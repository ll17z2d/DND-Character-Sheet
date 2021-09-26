using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using DND_Character_Sheet.Commands;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.Serialization;
using DND_Character_Sheet.Views;
using DND_Character_Sheet.Wrappers;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;

namespace DND_Character_Sheet.ViewModels
{
    public class HomeScreenViewModel
    {
        public IDialogWindowWrapper DialogWindowWrapper { get; set; }

        public ITextFormatterWrapper TextFormatterWrapper { get; set; }

        public ISerializeCharacterWrapper SerializeCharacterWrapper { get; set; }

        public IWindowWrapper CharacterCreatorWindowWrapper { get; set; }

        public IWindowWrapper CharacterSheetWindowWrapper { get; set; }

        public ICharacterModel Character { get; set; }

        public ICommand NewCharacterCommand { get; set; }

        public ICommand OpenCharacterCommand { get; set; }

        //public ICommand EditCharacterCommand { get; set; }

        public HomeScreenViewModel(IDialogWindowWrapper dialogWindowWrapper, ITextFormatterWrapper textFormatterWrapper, ISerializeCharacterWrapper serializeCharacterWrapper)
        {
            NewCharacterCommand = new MethodCommands(NewCharacter);
            OpenCharacterCommand = new MethodCommands(OpenCharacter);
            DialogWindowWrapper = dialogWindowWrapper;
            TextFormatterWrapper = textFormatterWrapper;
            SerializeCharacterWrapper = serializeCharacterWrapper;
            
            //CharacterSheetWindowWrapper = new WindowWrapper(new CharacterSheetView(Character, DialogWindowWrapper,
            //    TextFormatterWrapper, SerializeCharacterWrapper));
            //CharacterCreatorWindowWrapper =
            //    new WindowWrapper(new CharacterCreatorView(DialogWindowWrapper, TextFormatterWrapper,
            //        SerializeCharacterWrapper));

            InitialiseCharacterSheetWindowWrapper();
            InitialiseCharacterCreatorWindowWrapper();

            //MessageBoxWrapper.Show("peta griffin", "peter alert", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.None);
        }

        private void InitialiseCharacterCreatorWindowWrapper() 
            => CharacterCreatorWindowWrapper =
                new WindowWrapper(
                    new CharacterCreatorView(DialogWindowWrapper, TextFormatterWrapper, SerializeCharacterWrapper));

        private void InitialiseCharacterSheetWindowWrapper() 
            => CharacterSheetWindowWrapper = new WindowWrapper();

        public bool OpenCharacter()
        {
            DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.Filter = "DND Characters|*.json|All files|*.*";
            DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var dialogResult = DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.ShowDialog();

            if (dialogResult == true)
            {
                Character = Open(DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.FileName);
                //CharacterSheetWindowWrapper.Window = new CharacterSheetView(Character, DialogWindowWrapper,
                //    TextFormatterWrapper, SerializeCharacterWrapper);
                CharacterSheetWindowWrapper = CreateCharacterSheetWindowWrapper();
                CharacterSheetWindowWrapper.ShowDialog();


                //new CharacterSheetView(character, DialogWindowWrapper, TextFormatterWrapper, SerializeCharacterWrapper).ShowDialog();
            }

            return (bool)dialogResult;

        }

        public bool NewCharacter()
        {
            InitialiseCharacterCreatorWindowWrapper();
            var dialogResult = CharacterCreatorWindowWrapper.ShowDialog();

            return (bool)dialogResult;
        }

        private ICharacterModel Open(string filePath)
            => SerializeCharacterWrapper.OpenCharacterFromFile(filePath);

        private IWindowWrapper CreateCharacterSheetWindowWrapper()
        {
            return new WindowWrapper(new CharacterSheetView(Character, DialogWindowWrapper,
                TextFormatterWrapper, SerializeCharacterWrapper));
        }
    }
}
