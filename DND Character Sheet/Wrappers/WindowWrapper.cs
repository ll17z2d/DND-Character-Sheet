using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DND_Character_Sheet.Wrappers
{
    public interface IWindowWrapper : IFileDialogWrapper
    {
        public Window Window { get; set; }
        public new bool? ShowDialog();
        public void Show();
    }

    public class WindowWrapper : IWindowWrapper
    {
        public WindowWrapper() { }
        public WindowWrapper(Window window) 
            => Window = window;

        public Window Window { get; set; }

        public bool? ShowDialog() 
            => Window.ShowDialog();

        public void Show() 
            => Window.Show();
    }
}
