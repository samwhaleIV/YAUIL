namespace YAUIL.Layout {
    public readonly record struct PaddingSet() {

        public PaddingSet(Size left,Size top,Size right,Size bottom) : this() {
            Left = left; Top = top; Right = right; Bottom = bottom;
        }

        public Size Left { get; init; } = new(0,SizeMode.Absolute);
        public Size Top { get; init; } = new(0,SizeMode.Absolute);
        public Size Right { get; init; } = new(0,SizeMode.Absolute);
        public Size Bottom { get; init; } = new(0,SizeMode.Absolute);

        public static implicit operator PaddingSet((Size Left, Size Top, Size Right, Size Bottom) paddingSet) {
            return new(paddingSet.Left,paddingSet.Top,paddingSet.Right,paddingSet.Bottom);
        }

        public static implicit operator PaddingSet(Size size) {
            return new(size,size,size,size);
        }

        public static implicit operator PaddingSet((Size SizeX,Size SizeY) value) {
            return new(value.SizeX,value.SizeY,value.SizeX,value.SizeY);
        }

        public static implicit operator PaddingSet(float size) {
            return new(size,size,size,size);
        }

        public static implicit operator PaddingSet((float SizeX,float SizeY) value) {
            return new(value.SizeX,value.SizeY,value.SizeX,value.SizeY);
        }

        public static implicit operator PaddingSet((float Value,SizeMode Mode) value) {
            return new(value,value,value,value);
        }
    }
}
