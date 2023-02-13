namespace YAUIL {
    internal readonly record struct CoordinateBounds(
        float ViewportOrigin,
        float ParentOrigin,
        float ViewportLimit,
        float ParentLimit
    );
}
