using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DND_Character_Sheet.Models.Serialize_Types;
using DND_Character_Sheet.ViewModels;
using DND_Character_Sheet.Wrappers;

namespace DND_Character_Sheet.Views
{
    /// <summary>
    /// Interaction logic for Character_Creator_View.xaml
    /// </summary>
    public partial class CharacterCreatorView : Window
    {
        public CharacterCreatorView(CharacterCreatorViewModel characterCreatorViewModel)
        {
            InitializeComponent();
            this.DataContext = characterCreatorViewModel;
            Closing += characterCreatorViewModel.ExitWindow;
        }
    }
}
