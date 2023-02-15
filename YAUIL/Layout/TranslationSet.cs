namespace YAUIL.Layout {
    public readonly record struct TranslationSet() {
        public TranslationSet(Size x,Size y) : this() { X = x; Y = y; }

        public Size X { get; init; } = new(0,SizeMode.Absolute);
        public Size Y { get; init; } = new(0,SizeMode.Absolute);

        public static implicit operator TranslationSet(float value) => new(value,value);
        public static implicit operator TranslationSet(Size value) => new(value,value);
        public static implicit operator TranslationSet((float X, float Y) value) => new(value.X,value.Y);
        public static implicit operator TranslationSet((Size X, Size Y) value) => new(value.X,value.Y);
    }
}
