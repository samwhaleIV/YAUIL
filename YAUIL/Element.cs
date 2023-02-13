namespace YAUIL {
    public readonly record struct Element(
        ulong ID,
        Point Offset = default,
        Area Area = default,
        Padding Padding = default,
        AreaMode AreaMode = default,
        string? Name = null,
        ulong ParentID = uint.MinValue
    ) {
        public override string ToString() {
            return $"{(string.IsNullOrEmpty(Name) ? "<No Name>" : Name)}: ID/{ID}";
        }
    }
}
