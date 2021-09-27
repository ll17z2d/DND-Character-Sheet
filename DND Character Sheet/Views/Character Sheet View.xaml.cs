using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DND_Character_Sheet.Models;
using DND_Character_Sheet.Models.API_Models;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.ViewModels;
using DND_Character_Sheet.Wrappers;

namespace DND_Character_Sheet.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CharacterSheetView : Window
    {
        public CharacterSheetView(CharacterSheetViewModel characterSheetViewModel)
        {
            InitializeComponent();
            this.DataContext = characterSheetViewModel;
            Closing += characterSheetViewModel.ExitWindow;
        }

        //public CharacterSheetView(ICharacterModel character, IDialogWindowWrapper dialogWindowWrapper, ITextFormatterWrapper textFormatterWrapper, ISerializeCharacterWrapper serializeCharacterWrapper)
        //{
        //    InitializeComponent();
        //    var vm = new CharacterSheetViewModel(character, dialogWindowWrapper, textFormatterWrapper, serializeCharacterWrapper);
        //    this.DataContext = vm;
        //    Closing += vm.ExitWindow;
        //}


    }
}
