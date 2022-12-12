using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WpfButtonIcon.Controls;

namespace WpfButtonIcon;

class MainWindowViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        return true;
    }

    private sealed class MyCommand : ICommand
    {
        private readonly Action _action;
        private readonly Func<bool>? _canExecute;
        public event EventHandler? CanExecuteChanged;
        public MyCommand(Action execute, Func<bool>? canExecute = null) => (_action, _canExecute) = (execute, canExecute);
        public void Execute(object? parameter) => _action();
        public bool CanExecute(object? parameter) => (_canExecute is null) || _canExecute();
        public void ChangeCanExecute() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    private sealed class MyCommand<T> : ICommand
    {
        private readonly Action<T?> _action;
        private readonly Func<bool>? _canExecute;
        public event EventHandler? CanExecuteChanged;
        public MyCommand(Action<T?> execute, Func<bool>? canExecute = null) => (_action, _canExecute) = (execute, canExecute);
        public void Execute(object? parameter) => _action(parameter is not null ? (T)parameter : default);
        public bool CanExecute(object? parameter) => (_canExecute is null) || _canExecute();
        public void ChangeCanExecute() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public ICommand SingleClickCommand => _singleClickCommand ??= new MyCommand<object>(x => Message = $"Clicked {x}");
    ICommand? _singleClickCommand;

    public string Message
    {
        get => _message;
        set
        {
            if (SetProperty(ref _message, value))
                Debug.WriteLine(value);
        }
    }
    string _message = "";

    public IReadOnlyList<object> Icons { get; } = Enum.GetValues(typeof(PackIconKind)).OfType<object>().ToArray();

    public ICommand DoubleClickCommand => _doubleClickCommand ??= new MyCommand(() => Message = "Double click");
    ICommand? _doubleClickCommand;
}
