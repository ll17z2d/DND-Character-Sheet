using DND_Character_Sheet.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace DND_Character_Sheet.Views
{
    /// <summary>
    /// Interaction logic for Details_Dialog_View.xaml
    /// </summary>
    public partial class DetailsDialogView : Window, IView
    {
        public DetailsDialogView(DetailsDialogViewModel detailsDialogViewModel, Action<object, CancelEventArgs> removeWindowFromActiveList)
        {
            InitializeComponent();
            this.DataContext = detailsDialogViewModel;
            Closing += removeWindowFromActiveList.Invoke;
        }
    }
}
