namespace YAUIL.Layout {
    internal static class TranslationHelper {
        
        /* She ain't pretty, but she's honest. */

        internal static float TranslateByCoordinateMode(float value,float length,CoordinateBounds bounds,CoordinateMode coordinateMode,Point viewportSize,Point parentSize) => coordinateMode switch {
            CoordinateMode.Viewport => bounds.ViewportOrigin + value,
            CoordinateMode.Parent => bounds.ParentOrigin + value,

            CoordinateMode.ViewportWidth => bounds.ParentOrigin + value * viewportSize.X,
            CoordinateMode.ViewportHeight => bounds.ParentOrigin + value * viewportSize.Y,

            CoordinateMode.ParentWidth => bounds.ParentOrigin + value * parentSize.X,
            CoordinateMode.ParentHeight => bounds.ParentOrigin + value * parentSize.Y,

            CoordinateMode.ViewportRTL => bounds.ViewportLimit - value - length,
            CoordinateMode.ParentRTL => bounds.ParentOrigin + bounds.ParentLimit - value - length,

            CoordinateMode.ViewportWidthRTL => bounds.ParentOrigin + (1 - value) * viewportSize.X -length,
            CoordinateMode.ViewportHeightRTL => bounds.ParentOrigin + (1 - value) * viewportSize.Y - length,

            CoordinateMode.ParentWidthRTL => bounds.ParentOrigin + (1 - value) * parentSize.X - length,
            CoordinateMode.ParentHeightRTL => bounds.ParentOrigin + (1 - value) * parentSize.Y - length,
            _ => 0
        };

        internal static float TranslateBySizeMode(float value,SizeMode sizeMode,Point viewportSize,Point parentSize) => sizeMode switch {
            SizeMode.Absolute => value,

            SizeMode.ViewportWidth => value * viewportSize.X,
            SizeMode.ViewportHeight => value * viewportSize.Y,

            SizeMode.ParentWidth => value * parentSize.X,
            SizeMode.ParentHeight => value * parentSize.Y,
            _ => 0
        };

        internal static Area TranslateAreaByPadding(Area area,Padding padding,Point viewportSize,Point parentSize) {
            float left = area.X + TranslateBySizeMode(padding.Left,padding.LeftMode,viewportSize,parentSize);
            float top = area.Y + TranslateBySizeMode(padding.Top,padding.TopMode,viewportSize,parentSize);

            float right = area.Right - TranslateBySizeMode(padding.Right,padding.RightMode,viewportSize,parentSize);
            float bottom = area.Bottom - TranslateBySizeMode(padding.Bottom,padding.BottomMode,viewportSize,parentSize);

            return new Area(left,top,right-left,bottom-top);
        }

        internal static Area TranslateAreaByTransform(Area area,Transform transform,Point viewportSize,Point parentSize) {

            Point scaledSize = area.Size * transform.Scale;

            Point origin = area.Origin + scaledSize * transform.Origin;
                    
            float x = origin.X + TranslateBySizeMode(transform.Translation.X,transform.TranslationModeX,viewportSize,parentSize);
            float y = origin.Y + TranslateBySizeMode(transform.Translation.Y,transform.TranslationModeY,viewportSize,parentSize);

            area = new(x,y,scaledSize.X,scaledSize.Y);
            return area;
        }

        internal static ElementOutput GetElementOutput(Element element,Area viewportArea,Area parentArea) {
            Point viewportSize = viewportArea.Size;
            Point parentSize = parentArea.Size;

            float width = TranslateBySizeMode(element.Area.Width,element.AreaMode.Width,viewportSize,parentSize),
                 height = TranslateBySizeMode(element.Area.Height,element.AreaMode.Height,viewportSize,parentSize);

            /* This wouldn't be so redundant if it weren't for the fact that X and Y can have independent coordinate modes. This way, at least, avoids branching. */

            CoordinateBounds xBounds = new() {
                ViewportOrigin = viewportArea.X,ViewportLimit = viewportArea.Right,ParentOrigin = parentArea.X,ParentLimit = parentArea.Right
            }, yBounds = new() {
                ViewportOrigin = viewportArea.Y,ViewportLimit = viewportArea.Bottom,ParentOrigin = parentArea.Y,ParentLimit = parentArea.Bottom
            };

            float x = TranslateByCoordinateMode(element.Area.X,width,xBounds,element.AreaMode.X,viewportSize,parentSize),
                  y = TranslateByCoordinateMode(element.Area.Y,height,yBounds,element.AreaMode.Y,viewportSize,parentSize);

            Area outputArea = TranslateAreaByPadding(new Area(x,y,width,height),element.Padding,viewportSize,parentSize);
            outputArea = TranslateAreaByTransform(outputArea,element.Transform,viewportSize,parentSize);

            return new ElementOutput(element.ID,outputArea);
        }
    }
}
