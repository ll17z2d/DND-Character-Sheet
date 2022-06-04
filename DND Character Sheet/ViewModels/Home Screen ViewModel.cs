using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
//using Windows.ApplicationModel.Resources.Core;
using DND_Character_Sheet.Commands;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.Wrappers;
using Microsoft.Xaml.Behaviors.Core;
//using Windows.Services.Store;
//using Windows.UI.Notifications;
//using ABI.Windows.Data.Xml.Dom;
//using GalaSoft.MvvmLight.Command;
//using XmlNodeList = Windows.Data.Xml.Dom.XmlNodeList;

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
            DonateCommand = new MethodCommands(DonateToMe);
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

        public bool DonateToMe()
        {
            //var storeContext = StoreContext.GetDefault();
            //var x = await storeContext.GetAssociatedStoreProductsAsync(new[] {"Durable"});
            //var result = await storeContext.RequestPurchaseAsync("DonationToZakID");

            //if (result.Status == StorePurchaseStatus.Succeeded)
            //{
                //var tileDocument = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text03);
                //XmlNodeList textNodes = tileDocument.GetElementsByTagName("text");
                //textNodes[0].InnerText = "Donation To Zak";
                //textNodes[1].InnerText = "Thanks so much for the donation";

                //TileNotification tileNotification = new TileNotification(tileDocument);
                //TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
            //}

            Process.Start(new ProcessStartInfo()
            {
                FileName = "https://www.paypal.com/donate?hosted_button_id=9DCVKW5QALNR4",
                UseShellExecute = true
            });
            //return result;
            return true;
        }

        //private async StorePurchaseResult temp(StoreContext storeContext)
        //{
        //    return await StorePurchaseResult(storeContext);
        //}

        //private static async Task<StorePurchaseResult> StorePurchaseResult(StoreContext storeContext)
        //{
        //    var result = await storeContext.RequestPurchaseAsync("DonationToZakID");
        //    return result;
        //}

        private ICharacterModel Open(string filePath)
            => StaticClassWrapper.SerializeCharacterWrapper.OpenCharacterFromFileJSON(filePath);
    }
}
