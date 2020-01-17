using System;
using System.Windows.Input;

namespace WPFCefSharpDemo.Assists
{
    public class DelegateCommand<TParameter> : ICommand
    {
        public DelegateCommand(
            Action<TParameter> executeAction,
            Func<TParameter, bool> canExecuteFunc = null)
        {
            this.ExecuteAction = executeAction;
            this.CanExecuteFunc = canExecuteFunc;
        }

        protected Action<TParameter> ExecuteAction { get; }

        protected Func<TParameter, bool> CanExecuteFunc { get; }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
            => this.CanExecuteFunc?.Invoke((TParameter)parameter) ?? true;

        public void Execute(object parameter)
            => this.ExecuteAction?.Invoke((TParameter)parameter);
    }
}
