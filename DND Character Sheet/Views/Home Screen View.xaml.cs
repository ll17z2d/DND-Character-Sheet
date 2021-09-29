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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using DND_Character_Sheet.ViewModels;
using DND_Character_Sheet.Wrappers;
using Microsoft.Win32;

namespace DND_Character_Sheet.Views
{
    /// <summary>
    /// Interaction logic for Home_Screen_View.xaml
    /// </summary>
    public partial class HomeScreenView : Window
    {
        public HomeScreenView()
        {
            InitializeComponent();
            var dialogWindowWrapper = 

            this.DataContext = new HomeScreenViewModel(new DialogWindowWrapper(
                new OpenFileDialogWrapper(new OpenFileDialog()),
                new SaveFileDialogWrapper(new SaveFileDialog()),
                new MessageBoxWrapper()), 
                new StaticClassWrapper(new TextFormatterWrapper(),
                new SerializeCharacterWrapper()),
                new OpenNewViewWrapper());
        }
    }
}
