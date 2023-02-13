namespace YAUIL {
    public readonly record struct ElementID(ulong Value) {
        public override string ToString() => Value.ToString();
        public static readonly ElementID None = new(ulong.MinValue);
        public static implicit operator ElementID(int value) => new((ulong)value);
    }
}
