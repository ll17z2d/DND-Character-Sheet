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

namespace DND_Character_Sheet.Views
{
    /// <summary>
    /// Interaction logic for Notes_Dialog_View.xaml
    /// </summary>
    public partial class NotesDialogView : Window
    {
        public NotesDialogView(NotesDialogViewModel notesDialogViewModel)
        {
            InitializeComponent();
            this.DataContext = notesDialogViewModel;
        }
    }
}
