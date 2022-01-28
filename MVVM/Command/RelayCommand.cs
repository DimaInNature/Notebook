using System;
using System.Windows.Input;

namespace MVVM.Command;

public sealed class RelayCommand : ICommand
{
    private readonly Action<object> _execute;

    private readonly Func<object, bool> _canExecute;

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object parameter) =>
        _canExecute == null || _canExecute(parameter);

    public void Execute(object parameter) =>
        _execute?.Invoke(parameter);

    public RelayCommand(Action<object> executeAction)
        : this(executeAction, null) =>
        _execute = executeAction;

    public RelayCommand(Action<object> executeAction,
        Func<object, bool> canExecuteFunc) =>
        (_canExecute, _execute) = (canExecuteFunc, executeAction);
}