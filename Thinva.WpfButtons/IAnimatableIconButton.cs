using System.Windows;
using System.Windows.Media;

namespace Thinva.WpfButtons;

public interface IAnimatableIconButton
{
    /// <summary>
    /// Icon
    /// </summary>
    PackIconKind Kind { get; }

    /// <summary>
    /// InactiveBrush
    /// </summary>
    static readonly DependencyProperty InactiveBrushProperty = default!;
    Brush InactiveBrush { get; set; }

    /// <summary>
    /// ActiveBrush
    /// </summary>
    static readonly DependencyProperty ActiveBrushProperty = default!;
    Brush ActiveBrush { get; set; }

    /// <summary>
    /// ClickBrush
    /// </summary>
    static readonly DependencyProperty ClickBrushProperty = default!;
    SolidColorBrush ClickBrush { get; set; }

    /// <summary>
    /// OpacityRatio
    /// </summary>
    static readonly DependencyProperty OpacityRatioProperty = default!;
    float OpacityRatio { get; set; }

    /// <summary>
    /// MouseOverColor (ReadOnlyDependencyProperty)
    /// </summary>
    Color MouseOverColor { get; }
}
