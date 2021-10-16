using System.Windows;
using DND_Character_Sheet.ViewModels;

namespace DND_Character_Sheet.Views
{
    /// <summary>
    /// Interaction logic for Skills_Dialog_Window.xaml
    /// </summary>
    public partial class SkillsDialogView : Window
    {
        public SkillsDialogView(SkillsDialogViewModel skillsDialogViewModel)
        {
            InitializeComponent();
            this.DataContext = skillsDialogViewModel;
        }
    }
}
