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
    }
}
