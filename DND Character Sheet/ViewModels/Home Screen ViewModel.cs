using System;
using System.Windows.Input;
using DND_Character_Sheet.Commands;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.Wrappers;

namespace DND_Character_Sheet.ViewModels
{
    public class HomeScreenViewModel
    {
        public IDialogWindowWrapper DialogWindowWrapper { get; set; }

        public IStaticClassWrapper StaticClassWrapper { get; set; }

        public IOpenNewViewWrapper OpenNewViewWrapper { get; set; }

        public ICharacterModel Character { get; set; }

        public ICommand NewCharacterCommand { get; set; }

        public ICommand OpenCharacterCommand { get; set; }

        //public ICommand EditCharacterCommand { get; set; }

        public HomeScreenViewModel(IDialogWindowWrapper dialogWindowWrapper, IStaticClassWrapper staticClassWrapper, IOpenNewViewWrapper openNewViewWrapper)
        {
            NewCharacterCommand = new MethodCommands(NewCharacter);
            OpenCharacterCommand = new MethodCommands(OpenCharacter);
            DialogWindowWrapper = dialogWindowWrapper;
            StaticClassWrapper = staticClassWrapper;
            OpenNewViewWrapper = openNewViewWrapper;

            //MessageBoxWrapper.Show("peta griffin", "peter alert", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.None);
        }

        public bool OpenCharacter()
        {
            DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.Filter = "DND Characters|*.json|All files|*.*";
            DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.FileName = "";

            var dialogResult = DialogWindowWrapper.OpenFileDialogWrapper.ShowDialog();

            if (dialogResult == true)
                OpenNewViewWrapper.OpenCharacterSheetWindow(
                    Open(DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.FileName),
                    DialogWindowWrapper, StaticClassWrapper, OpenNewViewWrapper);

            return dialogResult;
        }

        public bool NewCharacter() 
            => OpenNewViewWrapper.OpenCharacterCreatorWindow(
                DialogWindowWrapper, StaticClassWrapper, OpenNewViewWrapper);

        private ICharacterModel Open(string filePath)
            => StaticClassWrapper.SerializeCharacterWrapper.OpenCharacterFromFile(filePath);
    }
}
