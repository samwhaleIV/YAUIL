namespace YAUIL.Layout {
    public readonly record struct Transform() {
        public Point Origin { get; init; } = default;
        public Point Translation { get; init; } = default;

        public SizeMode TranslationModeX { get; init; } = SizeMode.Absolute;
        public SizeMode TranslationModeY { get; init; } = SizeMode.Absolute;

        public Point Scale { get; init; } = new Point() { X = 1, Y = 1 };
    }
}
