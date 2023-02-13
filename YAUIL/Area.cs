namespace YAUIL {
    public readonly record struct Area(float X,float Y,float Width,float Height) {
        public readonly Point Center => Origin + Size * 0.5f;
        public readonly Point Origin => new(X,Y);
        public readonly Point Size => new(Width,Height);
        public float Right => X + Width;
        public float Bottom => Y + Height;
    }
}
