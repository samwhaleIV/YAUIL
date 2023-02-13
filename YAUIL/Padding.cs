namespace YAUIL {
    public readonly record struct Padding(
        float Left = 0,
        float Right = 0,
        float Top = 0,
        float Bottom = 0,
        SizeMode LeftMode = SizeMode.Absolute,
        SizeMode RightMode = SizeMode.Absolute,
        SizeMode TopMode = SizeMode.Absolute,
        SizeMode BottomMode = SizeMode.Absolute
    ) {
        public static Padding All(float padding,SizeMode sizeMode = SizeMode.Absolute) => new() {
            Left = padding,
            Right = padding,
            Top = padding,
            Bottom = padding,
            LeftMode = sizeMode,
            RightMode = sizeMode,
            TopMode = sizeMode,
            BottomMode = sizeMode,
        };
    }
}
