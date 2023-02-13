namespace YAUIL.Layout {
    public readonly record struct Element() {

        public ElementID ID { get; init; } = ElementID.None;

        public Area Area { get; init; } = default;

        public Transform Transform { get; init; } = new Transform();
        public Padding Padding { get; init; } = new Padding();
        public AreaMode AreaMode { get; init; } = new AreaMode();

        public string? Name { get; init; } = null;
        public ElementID Parent { get; init; } = ElementID.None;

        public override string ToString() {
            return $"{(string.IsNullOrEmpty(Name) ? "<No Name>" : Name)}: ID/{ID}";
        }
    }
}
