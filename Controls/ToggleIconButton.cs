using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace WpfButtonIcon.Controls;

public sealed class ToggleIconButton : ToggleButton
{
    const float DefaultOpacityRatio = IconButtonUtils.DefaultOpacityRatio;

    static ToggleIconButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleIconButton), new FrameworkPropertyMetadata(typeof(ToggleIconButton)));
    }

    public ToggleIconButton()
    { }

    /// <summary>
    /// IconKindPair
    /// </summary>
    public static readonly DependencyProperty KindPairProperty =
        DependencyProperty.Register(nameof(KindPair), typeof(PackIconKindPair), typeof(ToggleIconButton),
            new UIPropertyMetadata(PackIconPair.BoxedDefaultKindPair,
                static (d, e) =>
                {
                    if (d is ToggleIconButton self && e.NewValue is PackIconKindPair pair)
                        (self.OffKind, self.OnKind) = PackIconPair.GetKinds(pair);
                }));
    public PackIconKindPair KindPair
    {
        get => (PackIconKindPair)GetValue(KindPairProperty);
        set => SetValue(KindPairProperty, value);
    }

    /// <summary>
    /// OffKind (ReadOnlyDependencyProperty)
    /// </summary>
    private static readonly DependencyPropertyKey OffKindPropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(OffKind), typeof(PackIconKind), typeof(ToggleIconButton), new PropertyMetadata(PackIconPair.BoxedDefaultKindOff));
    public static readonly DependencyProperty OffKindProperty = OffKindPropertyKey.DependencyProperty;
    public PackIconKind OffKind
    {
        get => (PackIconKind)GetValue(OffKindProperty);
        private set => SetValue(OffKindPropertyKey, value);
    }

    /// <summary>
    /// OnKind (ReadOnlyDependencyProperty)
    /// </summary>
    private static readonly DependencyPropertyKey OnKindPropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(OnKind), typeof(PackIconKind), typeof(ToggleIconButton), new PropertyMetadata(PackIconPair.BoxedDefaultKindOn));
    public static readonly DependencyProperty OnKindProperty = OnKindPropertyKey.DependencyProperty;
    public PackIconKind OnKind
    {
        get => (PackIconKind)GetValue(OnKindProperty);
        private set => SetValue(OnKindPropertyKey, value);
    }

    /// <summary>
    /// InactiveBrush
    /// </summary>
    public static readonly DependencyProperty InactiveBrushProperty =
        DependencyProperty.Register(nameof(InactiveBrush), typeof(Brush), typeof(ToggleIconButton), new PropertyMetadata(Brushes.Red));
    public Brush InactiveBrush
    {
        get => (Brush)GetValue(InactiveBrushProperty);
        set => SetValue(InactiveBrushProperty, value);
    }

    /// <summary>
    /// ActiveBrush
    /// </summary>
    public static readonly DependencyProperty ActiveBrushProperty =
        DependencyProperty.Register(nameof(ActiveBrush), typeof(Brush), typeof(ToggleIconButton), new PropertyMetadata(Brushes.Blue));
    public Brush ActiveBrush
    {
        get => (Brush)GetValue(ActiveBrushProperty);
        set => SetValue(ActiveBrushProperty, value);
    }

    /// <summary>
    /// ClickBrush
    /// </summary>
    public static readonly DependencyProperty ClickBrushProperty =
        DependencyProperty.Register(nameof(ClickBrush), typeof(SolidColorBrush), typeof(ToggleIconButton),
            new UIPropertyMetadata(Brushes.Green,
                static (d, e) =>
                {
                    if (d is ToggleIconButton self && e.NewValue is SolidColorBrush brush)
                        self.MouseOverColor = IconButtonUtils.GetMouseOverColor(brush.Color, self.OpacityRatio);
                }));
    public SolidColorBrush ClickBrush
    {
        get => (SolidColorBrush)GetValue(ClickBrushProperty);
        set => SetValue(ClickBrushProperty, value);
    }

    /// <summary>
    /// OpacityRatio
    /// </summary>
    public static readonly DependencyProperty OpacityRatioProperty =
        DependencyProperty.Register(nameof(OpacityRatio), typeof(float), typeof(ToggleIconButton),
            new FrameworkPropertyMetadata(DefaultOpacityRatio,
                static (d, e) =>
                {
                    if (d is ToggleIconButton self && e.NewValue is float opacityRatio)
                        self.MouseOverColor = IconButtonUtils.GetMouseOverColor(self.ClickBrush.Color, opacityRatio);
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
        DependencyProperty.RegisterReadOnly(nameof(MouseOverColor), typeof(Color), typeof(ToggleIconButton), new PropertyMetadata(Colors.Yellow));
    public static readonly DependencyProperty MouseOverColorProperty = MouseOverColorPropertyKey.DependencyProperty;
    public Color MouseOverColor
    {
        get => (Color)GetValue(MouseOverColorProperty);
        private set => SetValue(MouseOverColorPropertyKey, value);
    }
}
