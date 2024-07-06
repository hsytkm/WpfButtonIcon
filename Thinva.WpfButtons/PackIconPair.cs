namespace Thinva.WpfButtons;

public enum PackIconKindPair
{
    Face,
    LinkChain,
    RotateCircle,
    FileRotate,
}

internal static class PackIconPair
{
    internal static readonly object BoxedDefaultKindPair = PackIconKindPair.Face;

    static readonly Dictionary<PackIconKindPair, (PackIconKind Off, PackIconKind On)> _kinds = new()
    {
        [PackIconKindPair.Face] = (PackIconKind.Sad, PackIconKind.Happy),
        [PackIconKindPair.LinkChain] = (PackIconKind.LinkVariantOff, PackIconKind.LinkVariant),
        [PackIconKindPair.RotateCircle] = (PackIconKind.Reload, PackIconKind.Restore),
        [PackIconKindPair.FileRotate] = (PackIconKind.FileRotateLeftOutline, PackIconKind.FileRotateRightOutline),

    };

    internal static (PackIconKind Off, PackIconKind On) GetKinds(PackIconKindPair key) => _kinds[key];
}