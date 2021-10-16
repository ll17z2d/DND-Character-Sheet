using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
// ReSharper disable PossibleInvalidOperationException

namespace DND_Character_Sheet.Wrappers
{
    public interface IOpenFileDialogWrapper : IFileDialogWindowWrapper
    {
        public OpenFileDialog OpenFileDialog { get; set; }
    }

    public class OpenFileDialogWrapper : IOpenFileDialogWrapper
    {
        public OpenFileDialogWrapper(OpenFileDialog openFileDialog) 
            => OpenFileDialog = openFileDialog;

        public OpenFileDialog OpenFileDialog { get; set; }

        public bool ShowDialog() 
            => (bool)OpenFileDialog.ShowDialog();
    }
}
