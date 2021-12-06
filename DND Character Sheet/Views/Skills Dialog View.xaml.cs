using System;
using System.ComponentModel;
using System.Windows;
using DND_Character_Sheet.ViewModels;

namespace DND_Character_Sheet.Views
{
    /// <summary>
    /// Interaction logic for Skills_Dialog_Window.xaml
    /// </summary>
    public partial class SkillsDialogView : Window, IView
    {
        public SkillsDialogView(SkillsDialogViewModel skillsDialogViewModel, Action<object, CancelEventArgs> removeWindowFromActiveList)
        {
            InitializeComponent();
            this.DataContext = skillsDialogViewModel;
            Closing += removeWindowFromActiveList.Invoke;
        }
    }
}
