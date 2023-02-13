namespace YAUIL.Layout {
    public enum SizeMode {
        /// <summary>
        /// Pixel size. No translation.
        /// </summary>
        Absolute,
        /// <summary>
        /// Treat size as fractional value, relative to the width of <see cref="ElementContainer.Area"/>.
        /// </summary>
        ViewportWidth,
        /// <summary>
        /// Treats size as fractional value, relative to the height of <see cref="ElementContainer.Area"/>.
        /// </summary>
        ViewportHeight,
        /// <summary>
        /// Treat size as fractional value, relative to parent element's height. If no parent element, the same as <see cref="ViewportWidth"/>.
        /// </summary>
        ParentWidth,
        /// <summary>
        /// Treat size as fractional value, relative to parent element's height. If no parent element, the same as <see cref="ViewportHeight"/>.
        /// </summary>
        ParentHeight
    }
}
