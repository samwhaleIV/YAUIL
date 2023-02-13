namespace YAUIL {
    internal static class TranslationHelper {
        
        /* She ain't pretty, but she's honest. */

        internal static float TranslateByCoordinateMode(float value,float length,float offset,CoordinateBounds bounds,CoordinateMode coordinateMode,Point viewportSize,Point parentSize) => coordinateMode switch {
            CoordinateMode.Viewport => bounds.ViewportOrigin + value + offset * length,
            CoordinateMode.Parent => bounds.ParentOrigin + value + offset * length,

            CoordinateMode.ViewportWidth => bounds.ParentOrigin + value * viewportSize.X + offset * length,
            CoordinateMode.ViewportHeight => bounds.ParentOrigin + value * viewportSize.Y + offset * length,

            CoordinateMode.ParentWidth => bounds.ParentOrigin + value * parentSize.X + offset * length,
            CoordinateMode.ParentHeight => bounds.ParentOrigin + value * parentSize.Y + offset * length,

            CoordinateMode.ViewportRTL => bounds.ViewportLimit - value - (offset + 1) * length,
            CoordinateMode.ParentRTL => bounds.ParentOrigin + bounds.ParentLimit - value - (offset + 1) * length,

            CoordinateMode.ViewportWidthRTL => bounds.ParentOrigin + (1 - value) * viewportSize.X - (offset + 1) * length,
            CoordinateMode.ViewportHeightRTL => bounds.ParentOrigin + (1 - value) * viewportSize.Y - (offset + 1) * length,

            CoordinateMode.ParentWidthRTL => bounds.ParentOrigin + (1 - value) * parentSize.X - (offset + 1) * length,
            CoordinateMode.ParentHeightRTL => bounds.ParentOrigin + (1 - value) * parentSize.Y - (offset + 1) * length,
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

        internal static ElementOutput GetElementOutput(Element element,Area viewportArea,Area parentArea) {
            float width = TranslateBySizeMode(element.Area.Width,element.AreaMode.Width,viewportArea.Size,parentArea.Size),
                 height = TranslateBySizeMode(element.Area.Height,element.AreaMode.Height,viewportArea.Size,parentArea.Size);

            /* This wouldn't be so redundant if it weren't for the fact that X and Y can have independent coordinate modes. This way, at least, avoids branching. */

            CoordinateBounds xBounds = new() {
                ViewportOrigin = viewportArea.X,ViewportLimit = viewportArea.Right,ParentOrigin = parentArea.X,ParentLimit = parentArea.Right
            }, yBounds = new() {
                ViewportOrigin = viewportArea.Y,ViewportLimit = viewportArea.Bottom,ParentOrigin = parentArea.Y,ParentLimit = parentArea.Bottom
            };

            float x = TranslateByCoordinateMode(element.Area.X,width,element.Offset.X,xBounds,element.AreaMode.X,viewportArea.Size,parentArea.Size),
                  y = TranslateByCoordinateMode(element.Area.Y,height,element.Offset.Y,yBounds,element.AreaMode.Y,viewportArea.Size,parentArea.Size);

            return new ElementOutput(element.ID,TranslateAreaByPadding(new Area(x,y,width,height),element.Padding,viewportArea.Size,parentArea.Size));
        }
    }
}
