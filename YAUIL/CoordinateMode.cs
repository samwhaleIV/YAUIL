namespace YAUIL {
    public enum CoordinateMode {
        /// <summary>
        /// Relative only to <see cref="ElementContainer.Area"/>. Pixel value.
        /// </summary>
        Viewport,
        /// <summary>
        /// Fractional, normal value, relative to the width of <see cref="ElementContainer.Area"/>. Non-pixel value.
        /// </summary>
        ViewportWidth,
        /// <summary>
        /// Fractional, normal fractional value, relative to the height of <see cref="ElementContainer.Area"/>. Non-pixel value.
        /// </summary>
        ViewportHeight,
        /// <summary>
        /// Pixel coordinate, relative to the parent element's <see cref="Area"/>. If no parent element, the same as <see cref="Viewport"/>. Pixel value.
        /// </summary>
        Parent,
        /// <summary>
        /// Treat coordinate as fractional value, relative to parent element's width. If no parent element, the same as <see cref="ViewportWidth"/>. Non-pixel value.
        /// </summary>
        ParentWidth,
        /// <summary>
        /// Treat coordinate as fractional value, relative to parent element's height. If no parent element, the same as <see cref="ViewportHeight"/>. Non-pixel value.
        /// </summary>
        ParentHeight,

        /// <summary>
        /// Right-to-left version of <see cref="Viewport"/>. Inverted <see cref="Element.Offset"/>. Offset by additional <c>-1*Length</c>. Pixel value.
        /// </summary>
        ViewportRTL,
        /// <summary>
        /// Right-to-left version of <see cref="ParentWidth"/>. Inverted <see cref="Element.Offset"/>. Offset by additional <c>-1*Length</c>. Pixel value.
        /// </summary>
        ParentWidthRTL,
        /// <summary>
        /// Right-to-left version of <see cref="ViewportWidth"/>. Inverted <see cref="Element.Offset"/>. Offset by additional <c>-1*Length</c>. Normal coordinate is altered: <c>1-value</c>. Non-pixel value.
        /// </summary>
        ViewportWidthRTL,
        /// <summary>
        /// Right-to-left version of <see cref="ViewportHeight"/>. Inverted <see cref="Element.Offset"/>. Offset by additional <c>-1*Length</c>. Normal coordinate is altered: <c>1-value</c>. Non-pixel value.
        /// </summary>
        ViewportHeightRTL,
        /// <summary>
        /// Right-to-left version of <see cref="Parent"/>. Inverted <see cref="Element.Offset"/>. Offset by additional <c>-1*Length</c>. Normal coordinate is altered <c>1-value</c>. Non-pixel value.
        /// </summary>
        ParentRTL,
        /// <summary>
        /// Right-to-left version of <see cref="ParentHeight"/>.  Inverted <see cref="Element.Offset"/>. Offset by additional <c>-1*Length</c>. Normal coordinate is altered <c>1-value</c>. Non-pixel value.
        /// </summary>
        ParentHeightRTL
    }
}
