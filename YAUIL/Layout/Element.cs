namespace YAUIL.Layout {
    public readonly record struct Element() {

        public string? Name { get; init; } = null;
        public ElementID ID { get; init; } = ElementID.None;
        public ElementID Parent { get; init; } = ElementID.None;

        public Size Width { get; init; } = new(0,SizeMode.Absolute);
        public Size Height { get; init; } = new(0,SizeMode.Absolute);

        public Coordiante X { get; init; } = new(0,CoordinateMode.Parent);
        public Coordiante Y { get; init; } = new(0,CoordinateMode.Parent);

        public Size PaddingLeft { get; init; } = new(0,SizeMode.Absolute);
        public Size PaddingTop { get; init; } = new(0,SizeMode.Absolute);
        public Size PaddingRight { get; init; } = new(0,SizeMode.Absolute);
        public Size PaddingBottom { get; init; } = new(0,SizeMode.Absolute);

        public Point Scale { get; init; } = new(1,1);
        public Point OriginOffset { get; init; } = new(0,0);

        public Size TranslationX { get; init; } = new(0,SizeMode.Absolute);
        public Size TranslationY { get; init; } = new(0,SizeMode.Absolute);

        public (Coordiante X,Coordiante Y,Size Width,Size Height) Area {
            init { X = value.X; Y = value.Y; Width = value.Width; Height = value.Height; }
        }

        public PaddingSet Padding {
            init { PaddingLeft = value.Left; PaddingTop = value.Top; PaddingRight = value.Right; PaddingBottom = value.Bottom; }
        }
        

        public override string ToString() {
            return $"{(string.IsNullOrEmpty(Name) ? "<No Name>" : Name)}: ID/{ID}";
        }
    }
}
