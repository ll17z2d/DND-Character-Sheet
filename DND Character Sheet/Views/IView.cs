using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DND_Character_Sheet.Views
{
    public interface IView
    {
        public void Show();

        public void Close();

        public object DataContext { get; set; }
    }
}
