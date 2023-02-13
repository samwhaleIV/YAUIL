namespace YAUIL {
    public readonly record struct Area() {

        public float X { get; init; } = 0;
        public float Y { get; init; } = 0;
        public float Width { get; init; } = 0;
        public float Height { get; init; } = 0;

        public Area(float origin,float size):this() {
            X = origin;
            Y = origin;
            Width = size;
            Height = size;
        }

        public Area(float x,float y,float width,float height):this() {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public readonly Point Center => Origin + Size * 0.5f;
        public readonly Point Origin => new(X,Y);
        public readonly Point Size => new(Width,Height);

        public float Right => X + Width;
        public float Bottom => Y + Height;

        public static implicit operator Area(float value) => new() {
            X = value,
            Y = value,
            Width = value,
            Height = value
        };

        public static implicit operator Area((float Origin, float Size) area) => new(area.Origin,area.Size); 
    }
}
