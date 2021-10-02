using System.Windows;
using DND_Character_Sheet.ViewModels;

namespace DND_Character_Sheet.Views
{
    /// <summary>
    /// Interaction logic for Spells_Dialog_View.xaml
    /// </summary>
    public partial class SpellsDialogView : Window
    {
        public SpellsDialogView(SpellsDialogViewModel spellsDialogViewModel)
        {
            this.DataContext = spellsDialogViewModel;
            InitializeComponent();
        }
    }
}
