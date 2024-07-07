using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Thinva.WpfButtons;

public sealed class RepeatIconButton : RepeatButton, IAnimatableIconButton
{
    static RepeatIconButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(RepeatIconButton), new FrameworkPropertyMetadata(typeof(RepeatIconButton)));
    }

    public RepeatIconButton()
    { }

    /// <summary>
    /// IconKind
    /// </summary>
    public static readonly DependencyProperty KindProperty =
        DependencyProperty.Register(nameof(Kind), typeof(PackIconKind), typeof(RepeatIconButton), new PropertyMetadata(PackIcon.BoxedDefaultKind));
    public PackIconKind Kind
    {
        get => (PackIconKind)GetValue(KindProperty);
        set => SetValue(KindProperty, value);
    }

    /// <summary>
    /// InactiveBrush
    /// </summary>
    public static readonly DependencyProperty InactiveBrushProperty =
        DependencyProperty.Register(nameof(InactiveBrush), typeof(Brush), typeof(RepeatIconButton), new PropertyMetadata(Brushes.Red));
    public Brush InactiveBrush
    {
        get => (Brush)GetValue(InactiveBrushProperty);
        set => SetValue(InactiveBrushProperty, value);
    }

    /// <summary>
    /// ActiveBrush
    /// </summary>
    public static readonly DependencyProperty ActiveBrushProperty =
        DependencyProperty.Register(nameof(ActiveBrush), typeof(Brush), typeof(RepeatIconButton), new PropertyMetadata(Brushes.Blue));
    public Brush ActiveBrush
    {
        get => (Brush)GetValue(ActiveBrushProperty);
        set => SetValue(ActiveBrushProperty, value);
    }

    /// <summary>
    /// ClickBrush
    /// </summary>
    public static readonly DependencyProperty ClickBrushProperty =
        DependencyProperty.Register(nameof(ClickBrush), typeof(Brush), typeof(RepeatIconButton),
            new PropertyMetadata(Brushes.Green, static (d, e) =>
                {
                    if (d is RepeatIconButton self && e.NewValue is SolidColorBrush brush)
                        self.MouseOverColor = IconButtonUtils.GetOpacityChangedColor(brush.Color, self.OpacityRatio);
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
        DependencyProperty.Register(nameof(OpacityRatio), typeof(float), typeof(RepeatIconButton),
            new PropertyMetadata(IconButtonUtils.BoxedDefaultOpacityRatio, static (d, e) =>
                {
                    if (d is RepeatIconButton self && e.NewValue is float opacityRatio)
                        if (self.ClickBrush is SolidColorBrush brush)
                            self.MouseOverColor = IconButtonUtils.GetOpacityChangedColor(brush.Color, opacityRatio);
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
        DependencyProperty.RegisterReadOnly(nameof(MouseOverColor), typeof(Color), typeof(RepeatIconButton), new PropertyMetadata(Colors.Yellow));
    public static readonly DependencyProperty MouseOverColorProperty = MouseOverColorPropertyKey.DependencyProperty;
    public Color MouseOverColor
    {
        get => (Color)GetValue(MouseOverColorProperty);
        private set => SetValue(MouseOverColorPropertyKey, value);
    }
}
