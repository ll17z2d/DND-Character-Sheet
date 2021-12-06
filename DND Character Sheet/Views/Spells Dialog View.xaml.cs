using System;
using System.ComponentModel;
using System.Windows;
using DND_Character_Sheet.ViewModels;

namespace DND_Character_Sheet.Views
{
    /// <summary>
    /// Interaction logic for Spells_Dialog_View.xaml
    /// </summary>
    public partial class SpellsDialogView : Window, IView
    {
        public SpellsDialogView(SpellsDialogViewModel spellsDialogViewModel, Action<object, CancelEventArgs> removeWindowFromActiveList)
        {
            InitializeComponent();
            this.DataContext = spellsDialogViewModel;
            Closing += removeWindowFromActiveList.Invoke;
        }
    }
}
