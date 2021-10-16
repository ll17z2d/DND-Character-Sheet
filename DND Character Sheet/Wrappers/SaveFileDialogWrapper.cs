using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
// ReSharper disable PossibleInvalidOperationException

namespace DND_Character_Sheet.Wrappers
{
    public interface ISaveFileDialogWrapper : IFileDialogWindowWrapper
    {
        public SaveFileDialog SaveFileDialog { get; set; }
    }

    public class SaveFileDialogWrapper : ISaveFileDialogWrapper
    {
        public SaveFileDialogWrapper(SaveFileDialog saveFileDialog) 
            => SaveFileDialog = saveFileDialog;

        public SaveFileDialog SaveFileDialog { get; set; }

        public bool ShowDialog() 
            => (bool)SaveFileDialog.ShowDialog();
    }
}
