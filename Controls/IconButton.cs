using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfButtonIcon.Controls;

public sealed class IconButton : ButtonBase
{
    const float DefaultOpacityRatio = 0.7f;

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
    /// InactiveBrush
    /// </summary>
    public static readonly DependencyProperty InactiveBrushProperty =
        DependencyProperty.Register(nameof(InactiveBrush), typeof(Brush), typeof(IconButton), new PropertyMetadata(Brushes.Red));
    public Brush InactiveBrush
    {
        get => (Brush)GetValue(InactiveBrushProperty);
        set => SetValue(InactiveBrushProperty, value);
    }

    /// <summary>
    /// ActiveBrush
    /// </summary>
    public static readonly DependencyProperty ActiveBrushProperty =
        DependencyProperty.Register(nameof(ActiveBrush), typeof(Brush), typeof(IconButton), new PropertyMetadata(Brushes.Blue));
    public Brush ActiveBrush
    {
        get => (Brush)GetValue(ActiveBrushProperty);
        set => SetValue(ActiveBrushProperty, value);
    }

    /// <summary>
    /// ClickBrush
    /// </summary>
    public static readonly DependencyProperty ClickBrushProperty =
        DependencyProperty.Register(nameof(ClickBrush), typeof(Brush), typeof(IconButton),
            new UIPropertyMetadata(Brushes.Green,
                static (d, e) =>
                {
                    if (d is IconButton self && e.NewValue is SolidColorBrush brush)
                        UpdateMouseOverColor(self, brush.Color, self.OpacityRatio);
                }));
    public Brush ClickBrush
    {
        get => (Brush)GetValue(ClickBrushProperty);
        set => SetValue(ClickBrushProperty, value);
    }

    /// <summary>
    /// OpacityRatio
    /// </summary>
    public static readonly DependencyProperty OpacityRatioProperty =
        DependencyProperty.Register(nameof(OpacityRatio), typeof(float), typeof(IconButton),
            new FrameworkPropertyMetadata(DefaultOpacityRatio,
                static (d, e) =>
                {
                    if (d is IconButton self && self.ClickBrush is SolidColorBrush brush && e.NewValue is float opacityRatio)
                        UpdateMouseOverColor(self, brush.Color, opacityRatio);
                }));
    public float OpacityRatio

    {
        get => (float)GetValue(OpacityRatioProperty);
        set => SetValue(OpacityRatioProperty, value);
    }

    /// <summary>
    /// MouseOverColor (ReadOnlyDependencyProperty)
    /// </summary>
    private static readonly DependencyPropertyKey MouseOverColorPropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(MouseOverColor), typeof(Color), typeof(IconButton), new PropertyMetadata(Colors.Yellow));
    public static readonly DependencyProperty MouseOverColorProperty = MouseOverColorPropertyKey.DependencyProperty;
    public Color MouseOverColor
    {
        get => (Color)GetValue(MouseOverColorProperty);
        private set => SetValue(MouseOverColorPropertyKey, value);
    }
    static void UpdateMouseOverColor(IconButton self, Color color, float opacityRatio)
    {
        Color mouseOverColor = new()
        {
            A = (byte)Math.Clamp(color.A * opacityRatio, 0, 0xff),
            R = color.R,
            G = color.G,
            B = color.B,
        };
        self.MouseOverColor = mouseOverColor;
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
