using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace WpfButtonIcon.Controls;

public sealed class ToggleIconButton : ToggleButton
{
    sealed record CheckedProps(PackIconKind IconKind, Color MouseOverColor, Brush ActiveBrush);
    CheckedProps? _offProps;
    CheckedProps? _onProps;

    static ToggleIconButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleIconButton), new FrameworkPropertyMetadata(typeof(ToggleIconButton)));
    }

    public ToggleIconButton()
    {
        // Checked 変化時の処理を（ライブラリ導入なしの）xaml で実装できなかったのでコードビハインドで対応しました。
        Initialized += ToggleIconButton_Initialized;
        Checked += ToggleIconButton_Checked;
        Unchecked += ToggleIconButton_Unchecked;
    }

    static void ToggleIconButton_Initialized(object? sender, EventArgs e)
    {
        static void initProps(ToggleIconButton self)
        {
            (PackIconKind iconOff, PackIconKind iconOn) = PackIconPair.GetKinds(self.KindPair);
            Color clickColor = self.ClickBrush.Color;
            float checkedMouseOverOpacityRatio = self.OpacityRatio;

            Color checkedMouseOverColor = IconButtonUtils.GetOpacityChangedColor(clickColor, checkedMouseOverOpacityRatio);
            Color uncheckedMouseOverColor = IconButtonUtils.GetOpacityChangedColor(checkedMouseOverColor, checkedMouseOverOpacityRatio);  // apply Ratio twice.
            Color checkedActiveColor = uncheckedMouseOverColor;   // 揃える
            SolidColorBrush checkedActiveBrush = new(checkedActiveColor);
            checkedActiveBrush.Freeze();

            self._offProps = new CheckedProps(iconOff, uncheckedMouseOverColor, self.ActiveBrush);
            self._onProps = new CheckedProps(iconOn, checkedMouseOverColor, checkedActiveBrush);
            self.MouseOverColor = checkedMouseOverColor;
        }

        if (sender is ToggleIconButton self)
        {
            self.Initialized -= ToggleIconButton_Initialized;
            initProps(self);
            OnIsCheckedPropertyChanged(self);
        }
    }

    static void ToggleIconButton_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is ToggleIconButton self)
            OnIsCheckedPropertyChanged(self);
    }

    static void ToggleIconButton_Unchecked(object sender, RoutedEventArgs e)
    {
        if (sender is ToggleIconButton self)
            OnIsCheckedPropertyChanged(self);
    }

    static void OnIsCheckedPropertyChanged(ToggleIconButton self)
    {
        CheckedProps? props = (self.IsChecked ?? false) ? self._onProps : self._offProps;
        if (props is not null)
        {
            self.Kind = props.IconKind;
            self.MouseOverColor = props.MouseOverColor;
            self.ActiveBrush = props.ActiveBrush;
        }
    }

    /// <summary>
    /// IconKindPair
    /// </summary>
    public static readonly DependencyProperty KindPairProperty =
        DependencyProperty.Register(nameof(KindPair), typeof(PackIconKindPair), typeof(ToggleIconButton), new PropertyMetadata(PackIconPair.BoxedDefaultKindPair));
    public PackIconKindPair KindPair
    {
        get => (PackIconKindPair)GetValue(KindPairProperty);
        set => SetValue(KindPairProperty, value);
    }

    /// <summary>
    /// Kind (ReadOnlyDependencyProperty)
    /// </summary>
    private static readonly DependencyPropertyKey KindPropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(Kind), typeof(PackIconKind), typeof(ToggleIconButton), new PropertyMetadata(PackIcon.BoxedDefaultKind));
    public static readonly DependencyProperty KindProperty = KindPropertyKey.DependencyProperty;
    public PackIconKind Kind
    {
        get => (PackIconKind)GetValue(KindProperty);
        private set => SetValue(KindPropertyKey, value);
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
        DependencyProperty.Register(nameof(ClickBrush), typeof(SolidColorBrush), typeof(ToggleIconButton), new PropertyMetadata(Brushes.Green));
    public SolidColorBrush ClickBrush
    {
        get => (SolidColorBrush)GetValue(ClickBrushProperty);
        set => SetValue(ClickBrushProperty, value);
    }

    /// <summary>
    /// OpacityRatio
    /// </summary>
    public static readonly DependencyProperty OpacityRatioProperty =
        DependencyProperty.Register(nameof(OpacityRatio), typeof(float), typeof(ToggleIconButton), new PropertyMetadata(IconButtonUtils.BoxedDefaultOpacityRatio));
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
