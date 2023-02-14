namespace YAUIL.Layout {
    internal static class TranslationHelper {
        
        /* She ain't pretty, but she's honest. */

        internal static float TranslateByCoordinateMode(Coordiante coordinate,float length,CoordinateBounds bounds,Point viewportSize,Point parentSize) => coordinate.Mode switch {
            CoordinateMode.Viewport => bounds.ViewportOrigin + coordinate.Value,
            CoordinateMode.Parent => bounds.ParentOrigin + coordinate.Value,

            CoordinateMode.ViewportWidth => bounds.ParentOrigin + coordinate.Value * viewportSize.X,
            CoordinateMode.ViewportHeight => bounds.ParentOrigin + coordinate.Value * viewportSize.Y,

            CoordinateMode.ParentWidth => bounds.ParentOrigin + coordinate.Value * parentSize.X,
            CoordinateMode.ParentHeight => bounds.ParentOrigin + coordinate.Value * parentSize.Y,

            CoordinateMode.ViewportRTL => bounds.ViewportLimit - coordinate.Value - length,
            CoordinateMode.ParentRTL => bounds.ParentOrigin + bounds.ParentLimit - coordinate.Value - length,

            CoordinateMode.ViewportWidthRTL => bounds.ParentOrigin + (1 - coordinate.Value) * viewportSize.X -length,
            CoordinateMode.ViewportHeightRTL => bounds.ParentOrigin + (1 - coordinate.Value) * viewportSize.Y - length,

            CoordinateMode.ParentWidthRTL => bounds.ParentOrigin + (1 - coordinate.Value) * parentSize.X - length,
            CoordinateMode.ParentHeightRTL => bounds.ParentOrigin + (1 - coordinate.Value) * parentSize.Y - length,
            _ => 0
        };

        internal static float TranslateBySizeMode(Size size,Point viewportSize,Point parentSize) => size.Mode switch {
            SizeMode.Absolute => size.Value,

            SizeMode.ViewportWidth => size.Value * viewportSize.X,
            SizeMode.ViewportHeight => size.Value * viewportSize.Y,

            SizeMode.ParentWidth => size.Value * parentSize.X,
            SizeMode.ParentHeight => size.Value * parentSize.Y,
            _ => 0
        };

        internal static Area TranslateAreaByPadding(Area area,Element element,Point viewportSize,Point parentSize) {
            float left = area.X + TranslateBySizeMode(element.PaddingLeft,viewportSize,parentSize);
            float top = area.Y + TranslateBySizeMode(element.PaddingTop,viewportSize,parentSize);

            float right = area.Right - TranslateBySizeMode(element.PaddingRight,viewportSize,parentSize);
            float bottom = area.Bottom - TranslateBySizeMode(element.PaddingBottom,viewportSize,parentSize);

            return new Area(left,top,right-left,bottom-top);
        }

        internal static Area TranslateAreaByTransform(Area area,Element element,Point viewportSize,Point parentSize) {

            Point scaledSize = area.Size * element.Scale;
            Point origin = area.Origin + scaledSize * element.OriginOffset;
                    
            float x = origin.X + TranslateBySizeMode(element.TranslationX,viewportSize,parentSize);
            float y = origin.Y + TranslateBySizeMode(element.TranslationY,viewportSize,parentSize);

            area = new(x,y,scaledSize.X,scaledSize.Y);
            return area;
        }

        internal static ElementOutput GetElementOutput(Element element,Area viewportArea,Area parentArea) {
            Point viewportSize = viewportArea.Size;
            Point parentSize = parentArea.Size;

            float width = TranslateBySizeMode(element.Width,viewportSize,parentSize),
                 height = TranslateBySizeMode(element.Height,viewportSize,parentSize);

            /* This wouldn't be so redundant if it weren't for the fact that X and Y can have independent coordinate modes. This way, at least, avoids branching. */

            CoordinateBounds xBounds = new() {
                ViewportOrigin = viewportArea.X,ViewportLimit = viewportArea.Right,ParentOrigin = parentArea.X,ParentLimit = parentArea.Right
            }, yBounds = new() {
                ViewportOrigin = viewportArea.Y,ViewportLimit = viewportArea.Bottom,ParentOrigin = parentArea.Y,ParentLimit = parentArea.Bottom
            };

            float x = TranslateByCoordinateMode(element.X,width,xBounds,viewportSize,parentSize),
                  y = TranslateByCoordinateMode(element.Y,height,yBounds,viewportSize,parentSize);

            Area outputArea = TranslateAreaByPadding(new Area(x,y,width,height),element,viewportSize,parentSize);
            outputArea = TranslateAreaByTransform(outputArea,element,viewportSize,parentSize);

            return new ElementOutput(element.ID,outputArea);
        }
    }
}
