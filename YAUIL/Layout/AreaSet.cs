namespace YAUIL.Layout {
    public readonly record struct AreaSet() {

        public AreaSet(Coordiante x,Coordiante y,Size width,Size height):this() {
            X = x; Y = y; Width = width; Height = height;
        }

        public Coordiante X { get; init; } = new(0,CoordinateMode.Parent);
        public Coordiante Y { get; init; } = new(0,CoordinateMode.Parent);

        public Size Width { get; init; } = new(0,SizeMode.Absolute);
        public Size Height { get; init; } = new(0,SizeMode.Absolute);

        public static implicit operator AreaSet((Coordiante X,Coordiante Y,Size Width,Size Height) value) {
            return new(value.X,value.Y,value.Width,value.Height);
        }

        public static implicit operator AreaSet((Coordiante X, Coordiante Y, Size Size) value) {
            return new(value.X,value.Y,value.Size,value.Size);
        }

        public static implicit operator AreaSet(((Coordiante X,Coordiante Y) Origin,Size Width, Size Height) value) {
            return new(value.Origin.X,value.Origin.Y,value.Width,value.Height);
        }

        public static implicit operator AreaSet(((Coordiante X, Coordiante Y) Origin,float Width,float Height) value) {
            return new(value.Origin.X,value.Origin.Y,value.Width,value.Height);
        }

        public static implicit operator AreaSet(((Coordiante X, Coordiante Y) Origin,float Size) value) {
            return new(value.Origin.X,value.Origin.Y,value.Size,value.Size);
        }

        public static implicit operator AreaSet((Coordiante X, Coordiante Y,(Size Width, Size Height) Size) value) {
            return new(value.X,value.Y,value.Size.Width,value.Size.Height);
        }

        public static implicit operator AreaSet(((Coordiante X, Coordiante Y) Origin,(Size Width, Size Height) Size) value) {
            return new(value.Origin.X,value.Origin.Y,value.Size.Width,value.Size.Height);
        }

        public static implicit operator AreaSet((Size Width, Size Height) size) {
            return new(0,0,size.Width,size.Height);
        }
    }
}
