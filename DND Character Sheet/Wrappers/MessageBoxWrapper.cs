using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DND_Character_Sheet.Wrappers
{
    public interface IMessageBoxWrapper
    {
        MessageBoxResult Show(string message, string messageBoxTitle, MessageBoxButton messageBoxButton, MessageBoxImage messageBoxImage, MessageBoxResult defaultMessageBoxResult);
    }

    public class MessageBoxWrapper : IMessageBoxWrapper
    {
        public MessageBoxResult Show(string message, string messageBoxTitle, MessageBoxButton messageBoxButton,
            MessageBoxImage messageBoxImage, MessageBoxResult defaultMessageBoxResult) =>
            MessageBox.Show(message, messageBoxTitle, messageBoxButton, messageBoxImage, defaultMessageBoxResult);
    }
}
