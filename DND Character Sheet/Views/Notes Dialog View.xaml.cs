using System;
using System.ComponentModel;
using System.Windows;
using DND_Character_Sheet.ViewModels;

namespace DND_Character_Sheet.Views
{
    /// <summary>
    /// Interaction logic for Notes_Dialog_View.xaml
    /// </summary>
    public partial class NotesDialogView : Window, IView
    {
        public NotesDialogView(NotesDialogViewModel notesDialogViewModel, Action<object, CancelEventArgs> removeWindowFromActiveList)
        {
            InitializeComponent();
            this.DataContext = notesDialogViewModel;
            Closing += removeWindowFromActiveList.Invoke;
        }
    }
}
