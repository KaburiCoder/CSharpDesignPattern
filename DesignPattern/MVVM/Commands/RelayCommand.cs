using System;
using System.Windows.Input;

namespace WpfTutorials.DesignPattern.MVVM.Commands
{
  public class RelayCommand<T> : ICommand
  {
    private readonly Action<T>? _execute;
    private readonly Predicate<T>? _canExecute;

    public RelayCommand(Action<T>? execute, Predicate<T>? canExecute = null)
    {
      this._execute = execute;
      this._canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object? parameter)
    {
      return _canExecute?.Invoke((T)parameter!) ?? true;
    }

    public void Execute(object? parameter)
    {
      _execute?.Invoke((T)parameter!);
    }
  }
}
