using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DND_Character_Sheet.Commands
{
    public class MethodCommands : ICommand
    {
        private Func<bool> executeMethod;
        private Func<bool> canExecuteMethod;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public MethodCommands(Func<bool> executeMethod) : this(executeMethod, () => true) { }

        public MethodCommands(Func<bool> executeMethod, Func<bool> canExecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            //var handler = CanExecuteChanged;
            //if (handler != null)
            //    handler(this, EventArgs.Empty);
            return canExecuteMethod();
            //return true;
        }

        public void Execute(object parameter)
        {
            executeMethod();
        }

    }
}
