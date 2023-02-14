namespace YAUIL.Layout {
    public readonly record struct Size() {

        public Size(float value,SizeMode mode) : this() { Value = value; Mode = mode; }

        public float Value { get; init; } = 0;
        public SizeMode Mode { get; init; } = SizeMode.Absolute;

        public static implicit operator Size((float Value, SizeMode Mode) value) => new(value.Value,value.Mode);
        public static implicit operator Size(float value) => new(value,SizeMode.Absolute);

        public static readonly Size ParentWidth = new(1,SizeMode.ParentWidth);
        public static readonly Size ParentHeight= new(1,SizeMode.ParentHeight);

        public static readonly Size ViewportWidth = new(1,SizeMode.ViewportWidth);
        public static readonly Size ViewportHeight = new(1,SizeMode.ViewportHeight);
    }
}
