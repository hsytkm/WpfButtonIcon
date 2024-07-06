using System.Windows.Media;

namespace Thinva.WpfButtons;

internal static class IconButtonUtils
{
    const float DefaultOpacityRatio = 0.7f;
    internal static readonly object BoxedDefaultOpacityRatio = DefaultOpacityRatio;

    internal static Color GetOpacityChangedColor(Color color, float opacityRatio) => new()
    {
        A = (byte)Math.Clamp(color.A * opacityRatio, 0, 0xff),
        R = color.R,
        G = color.G,
        B = color.B,
    };
}
