namespace YAUIL {
    public readonly record struct Point() { /* It's a floating point... get it? */

        public float X { get; init; } = 0;
        public float Y { get; init; } = 0;

        public Point(float value):this() { X = value; Y = value; }
        public Point(float x,float y) : this() { X = x; Y = y; }

        public static Point operator +(Point a,Point b) => new(a.X + b.X,a.Y + b.Y);
        public static Point operator -(Point a,Point b) => new(a.X - b.X,a.Y - b.Y);
        public static Point operator *(Point a,Point b) => new(a.X * b.X,a.Y * b.Y);
        public static Point operator /(Point a,Point b) => new(a.X / b.X,a.Y / b.Y);

        public static Point operator +(Point a,float b) => new(a.X + b,a.Y + b);
        public static Point operator -(Point a,float b) => new(a.X - b,a.Y - b);
        public static Point operator *(Point a,float b) => new(a.X * b,a.Y * b);
        public static Point operator /(Point a,float b) => new(a.X / b,a.Y / b);

        public static Point operator +(float a,Point b) => new(a + b.X,a + b.Y);
        public static Point operator -(float a,Point b) => new(a - b.X,a - b.Y);
        public static Point operator *(float a,Point b) => new(a * b.X,a * b.Y);
        public static Point operator /(float a,Point b) => new(a / b.X,a / b.Y);

        public static implicit operator Point(float value) => new(value);
        public static implicit operator Point((float X,float Y) point) => new(point.X,point.Y);
    }
}
