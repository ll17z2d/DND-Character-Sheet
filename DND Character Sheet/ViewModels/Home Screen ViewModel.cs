using System;
using System.Diagnostics;
using System.Windows.Input;
using DND_Character_Sheet.Commands;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.Wrappers;
using Microsoft.Xaml.Behaviors.Core;

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

        public ICommand DonateCommand { get; set; }

        //public ICommand EditCharacterCommand { get; set; }

        public HomeScreenViewModel(IDialogWindowWrapper dialogWindowWrapper, IStaticClassWrapper staticClassWrapper, IOpenNewViewWrapper openNewViewWrapper)
        {
            NewCharacterCommand = new MethodCommands(NewCharacter);
            OpenCharacterCommand = new MethodCommands(OpenCharacter);
            DonateCommand = new ActionCommand(DonateToMe);
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

            if (DialogWindowWrapper.OpenFileDialogWrapper.ShowDialog())
            {
                OpenNewViewWrapper.OpenCharacterSheetWindow(
                    Open(DialogWindowWrapper.OpenFileDialogWrapper.OpenFileDialog.FileName),
                    DialogWindowWrapper, StaticClassWrapper, OpenNewViewWrapper);
                return true;
            }

            return false;
        }

        public bool NewCharacter() 
            => OpenNewViewWrapper.OpenCharacterCreatorWindow(
                DialogWindowWrapper, StaticClassWrapper, OpenNewViewWrapper);

        public void DonateToMe()
            => Process.Start(new ProcessStartInfo()
            {
                FileName = "https://www.paypal.com/donate?hosted_button_id=9DCVKW5QALNR4",
                UseShellExecute = true
            }); //Process.Start(new ProcessStartInfo("cmd", $"/c start https://www.paypal.com/donate?hosted_button_id=9DCVKW5QALNR4") { CreateNoWindow = true });

        private ICharacterModel Open(string filePath)
            => StaticClassWrapper.SerializeCharacterWrapper.OpenCharacterFromFile(filePath);
    }
}
