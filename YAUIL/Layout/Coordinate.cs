namespace YAUIL.Layout {
    public readonly record struct Coordiante() {

        public Coordiante(float value,CoordinateMode mode) : this() {
            Value = value;
            Mode = mode;
        }

        public float Value { get; init; } = 0;
        public CoordinateMode Mode { get; init; } = CoordinateMode.Parent;

        public static implicit operator Coordiante((float Value, CoordinateMode Mode) value) {
            return new Coordiante(value.Value,value.Mode);
        }

        public static implicit operator Coordiante(float value) {
            return new Coordiante(value,CoordinateMode.Parent);
        }

        public static readonly Coordiante ParentCenterX = new(0.5f,CoordinateMode.ParentWidth);
        public static readonly Coordiante ParentCenterY = new(0.5f,CoordinateMode.ParentHeight);

        public static readonly Coordiante ViewportCenterX = new(0.5f,CoordinateMode.ViewportWidth);
        public static readonly Coordiante ViewportCenterY = new(0.5f,CoordinateMode.ViewportHeight);
    }
}
