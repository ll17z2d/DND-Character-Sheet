using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace DND_Character_Sheet.Wrappers
{
    public interface IDialogWindowWrapper
    {
        public IOpenFileDialogWrapper OpenFileDialogWrapper { get; set; }
        public ISaveFileDialogWrapper SaveFileDialogWrapper { get; set; }
        public IMessageBoxWrapper MessageBoxWrapper { get; set; }
    }

    public class DialogWindowWrapper : IDialogWindowWrapper
    {
        public DialogWindowWrapper(IOpenFileDialogWrapper openFileDialog, ISaveFileDialogWrapper saveFileDialog, IMessageBoxWrapper messageBoxWrapper)
        {
            OpenFileDialogWrapper = openFileDialog;
            SaveFileDialogWrapper = saveFileDialog;
            MessageBoxWrapper = messageBoxWrapper;
        }

        public IOpenFileDialogWrapper OpenFileDialogWrapper { get; set; }

        public ISaveFileDialogWrapper SaveFileDialogWrapper { get; set; }

        public IMessageBoxWrapper MessageBoxWrapper { get; set; }
    }
}
