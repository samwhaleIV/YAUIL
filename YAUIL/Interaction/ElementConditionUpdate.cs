namespace YAUIL.Interaction {
    internal readonly struct ElementConditionUpdate {
        public readonly ElementID ID { get; init; }
        public readonly ElementConditonChange Type { get; init; }
        public ElementConditionUpdate(ElementID ID,ElementConditonChange type) { this.ID = ID; Type = type; }
    }
}
