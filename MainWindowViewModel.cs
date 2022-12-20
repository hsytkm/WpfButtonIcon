using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WpfButtonIcon.Controls;

namespace WpfButtonIcon;

internal class MainWindowViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        return true;
    }

    public IReadOnlyList<PackIconKind> Icons { get; } = Enum.GetValues(typeof(PackIconKind)).OfType<PackIconKind>().ToArray();
    public IReadOnlyList<PackIconKindPair> IconPairs { get; } = Enum.GetValues(typeof(PackIconKindPair)).OfType<PackIconKindPair>().ToArray();
    
    int _counter;

    public ICommand SingleClickCommand => _singleClickCommand ??= new MyCommand<object>(x => Message = $"Clicked({_counter++}) {x}.");
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

    public bool EnableButton
    {
        get => _enableButton;
        set => SetProperty(ref _enableButton, value);
    }
    bool _enableButton = true;

    public ICommand DoubleClickCommand => _doubleClickCommand ??= new MyCommand(() => Message = $"Double click({_counter++}).");
    ICommand? _doubleClickCommand;
}
