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

        public IWindowServiceWrapper WindowServiceWrapper { get; set; }

        public ICharacterModel Character { get; set; }

        public ICommand NewCharacterCommand { get; set; }

        public ICommand OpenCharacterCommand { get; set; }

        //public ICommand EditCharacterCommand { get; set; }

        public HomeScreenViewModel(IDialogWindowWrapper dialogWindowWrapper, ITextFormatterWrapper textFormatterWrapper, ISerializeCharacterWrapper serializeCharacterWrapper, IWindowServiceWrapper windowServiceWrapper)
        {
            NewCharacterCommand = new MethodCommands(NewCharacter);
            OpenCharacterCommand = new MethodCommands(OpenCharacter);
            DialogWindowWrapper = dialogWindowWrapper;
            TextFormatterWrapper = textFormatterWrapper;
            SerializeCharacterWrapper = serializeCharacterWrapper;
            WindowServiceWrapper = windowServiceWrapper;

            //MessageBoxWrapper.Show("peta griffin", "peter alert", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.None);
        }

        public bool OpenCharacter()
        {
            DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.Filter = "DND Characters|*.json|All files|*.*";
            DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var dialogResult = DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.ShowDialog();

            if (dialogResult == true)
                WindowServiceWrapper.OpenCharacterSheetWindow(new CharacterSheetViewModel(
                    Open(DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.FileName),
                    DialogWindowWrapper, TextFormatterWrapper, SerializeCharacterWrapper, WindowServiceWrapper));

            return (bool)dialogResult;

        }

        public bool NewCharacter() 
            => (bool) WindowServiceWrapper.OpenCharacterCreatorWindow(
                new CharacterCreatorViewModel(DialogWindowWrapper, TextFormatterWrapper,
                    SerializeCharacterWrapper, WindowServiceWrapper));

        private ICharacterModel Open(string filePath)
            => SerializeCharacterWrapper.OpenCharacterFromFile(filePath);
    }
}
