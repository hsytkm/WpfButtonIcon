using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfButtonIcon.Controls;

public sealed class IconButton : ButtonBase
{
    static IconButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(IconButton), new FrameworkPropertyMetadata(typeof(IconButton)));
    }

    public IconButton()
    { }

    /// <summary>
    /// IconKind
    /// </summary>
    public static readonly DependencyProperty KindProperty =
        DependencyProperty.Register(nameof(Kind), typeof(object), typeof(IconButton), new PropertyMetadata(PackIcon.BoxedDefaultKind));
    public object Kind
    {
        get => (object)GetValue(KindProperty);
        set => SetValue(KindProperty, value);
    }

    /// <summary>
    /// DisableBrush
    /// </summary>
    public static readonly DependencyProperty DisableBrushProperty =
        DependencyProperty.Register(nameof(DisableBrush), typeof(Brush), typeof(IconButton), new PropertyMetadata(Brushes.Red));
    public Brush DisableBrush
    {
        get => (Brush)GetValue(DisableBrushProperty);
        set => SetValue(DisableBrushProperty, value);
    }

    /// <summary>
    /// EnableBrush
    /// </summary>
    public static readonly DependencyProperty EnableBrushProperty =
        DependencyProperty.Register(nameof(EnableBrush), typeof(Brush), typeof(IconButton), new PropertyMetadata(Brushes.Blue));
    public Brush EnableBrush
    {
        get => (Brush)GetValue(EnableBrushProperty);
        set => SetValue(EnableBrushProperty, value);
    }

    /// <summary>
    /// MouseOverBrush
    /// </summary>
    public static readonly DependencyProperty MouseOverBrushProperty =
        DependencyProperty.Register(nameof(MouseOverBrush), typeof(Brush), typeof(IconButton), new PropertyMetadata(Brushes.Green));
    public Brush MouseOverBrush
    {
        get => (Brush)GetValue(MouseOverBrushProperty);
        set => SetValue(MouseOverBrushProperty, value);
    }

    /// <summary>
    /// DoubleClickCommand
    /// </summary>
    public static readonly DependencyProperty DoubleClickCommandProperty =
        DependencyProperty.Register(nameof(DoubleClickCommand), typeof(ICommand), typeof(IconButton),
            new UIPropertyMetadata(null, (d, e) =>
            {
                if (d is IconButton button && e.NewValue is ICommand command)
                {
                    MouseBinding mouseBinding = new(command, new MouseGesture(MouseAction.LeftDoubleClick));
                    button.InputBindings.Add(mouseBinding);
                }
            }));
    public ICommand? DoubleClickCommand
    {
        get => (ICommand?)GetValue(DoubleClickCommandProperty);
        set => SetValue(DoubleClickCommandProperty, value);
    }
}
