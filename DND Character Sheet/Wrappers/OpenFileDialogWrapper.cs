using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace DND_Character_Sheet.Wrappers
{
    public interface IOpenFileDialogWrapper : IFileDialogWrapper
    {
        public OpenFileDialog OpenFileDialog { get; set; }
    }

    public class OpenFileDialogWrapper : IOpenFileDialogWrapper
    {
        public OpenFileDialogWrapper(OpenFileDialog openFileDialog) 
            => OpenFileDialog = openFileDialog;

        public virtual OpenFileDialog OpenFileDialog { get; set; }

        public bool? ShowDialog() 
            => OpenFileDialog.ShowDialog();
    }
}
