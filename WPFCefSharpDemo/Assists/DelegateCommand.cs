using System;
using System.Windows.Input;

namespace WPFCefSharpDemo.Assists
{
    public class DelegateCommand : ICommand
    {
        public DelegateCommand(
            Action executeAction,
            Func<bool> canExecuteFunc = null)
        {
            this.ExecuteAction = executeAction;
            this.CanExecuteFunc = canExecuteFunc;
        }

        protected Action ExecuteAction { get; }

        protected Func<bool> CanExecuteFunc { get; }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
            => this.CanExecuteFunc?.Invoke() ?? true;

        public void Execute(object parameter)
            => this.ExecuteAction?.Invoke();
    }
}
