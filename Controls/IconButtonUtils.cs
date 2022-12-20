using System;
using System.Windows.Media;

namespace WpfButtonIcon.Controls;

internal static class IconButtonUtils
{
    internal const float DefaultOpacityRatio = 0.7f;

    internal static Color GetMouseOverColor(Color color, float opacityRatio) => new()
    {
        A = (byte)Math.Clamp(color.A * opacityRatio, 0, 0xff),
        R = color.R,
        G = color.G,
        B = color.B,
    };
}
