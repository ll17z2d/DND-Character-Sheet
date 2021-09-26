using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace DND_Character_Sheet.Wrappers
{
    public interface ISaveFileDialogWrapper : IFileDialogWrapper
    {
        public SaveFileDialog SaveFileDialog { get; set; }

    }

    public class SaveFileDialogWrapper : ISaveFileDialogWrapper
    {
        public SaveFileDialogWrapper(SaveFileDialog saveFileDialog) 
            => SaveFileDialog = saveFileDialog;

        public SaveFileDialog SaveFileDialog { get; set; }

        public bool? ShowDialog() 
            => SaveFileDialog.ShowDialog();
    }
}
