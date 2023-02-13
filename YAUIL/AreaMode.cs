namespace YAUIL {
    public readonly record struct AreaMode(
        CoordinateMode X = CoordinateMode.Viewport,
        CoordinateMode Y = CoordinateMode.Viewport,

        SizeMode Width = SizeMode.Absolute,
        SizeMode Height = SizeMode.Absolute
    );
}
