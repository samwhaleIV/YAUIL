namespace YAUIL {
    public readonly record struct Point(float X,float Y) {
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
    }
}
