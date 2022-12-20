using System.Collections.Generic;

namespace WpfButtonIcon.Controls;

public enum PackIconKindPair
{
    Face,
    LinkChain,
    RotateCircle,
    FileRotate,
}

public static class PackIconPair
{
    public static readonly object BoxedDefaultKindPair = PackIconKindPair.Face;
    public static readonly object BoxedDefaultKindOff = PackIconKind.Sad;
    public static readonly object BoxedDefaultKindOn = PackIconKind.Happy;

    static readonly Dictionary<PackIconKindPair, (PackIconKind Off, PackIconKind On)> _kinds = new()
    {
        [PackIconKindPair.Face] = (PackIconKind.Sad, PackIconKind.Happy),
        [PackIconKindPair.LinkChain] = (PackIconKind.LinkVariantOff, PackIconKind.LinkVariant),
        [PackIconKindPair.RotateCircle] = (PackIconKind.Reload, PackIconKind.Restore),
        [PackIconKindPair.FileRotate] = (PackIconKind.FileRotateLeftOutline, PackIconKind.FileRotateRightOutline),

    };

    public static (PackIconKind Off, PackIconKind On) GetKinds(PackIconKindPair key) => _kinds[key];
}