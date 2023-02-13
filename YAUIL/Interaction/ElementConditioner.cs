namespace YAUIL.Interaction {
    public abstract class ElementConditioner {

        public ElementConditioner(int maxSelectionCount = Constants.DefaultMaxElementCount) {
            _maxSelectionCount = maxSelectionCount;
        }

        private readonly int _maxSelectionCount;
        public int MaxSelectionCount => _maxSelectionCount;

        private readonly HashSet<ElementID> _selectedElements = new();
        public IEnumerable<ElementID> GetSelectedElements() => _selectedElements;

        public int SelectedElementCount => _selectedElements.Count;

        public bool AllowMultipleSelections { get; init; }
        public bool ConditionsPaused { get; set; } = false;

        public ElementID? CapturedElement { get; private set; } = null;
        public ElementID? SelectedElement { get; private set; } = null;

        protected void AddSelectedElement(ElementID elementID,bool clearSelectionList = true) {
            if(ConditionsPaused || _selectedElements.Contains(elementID)) {
                return;
            }
            if(!AllowMultipleSelections || clearSelectionList) {
                foreach(var ID in _selectedElements) {
                    _elementConditionUpdates.Enqueue(new(ID,ElementConditonChange.SelectionRemoved));
                }
                _selectedElements.Clear();
            }
            SelectedElement = elementID;    
            _selectedElements.Add(elementID);
            _elementConditionUpdates.Enqueue(new(elementID,ElementConditonChange.SelectionAdded));
        }

        protected void ClearSelectedElements() {
            ElementID? selectedElement = SelectedElement;
            if(ConditionsPaused || !selectedElement.HasValue) {
                return;
            }
            foreach(var ID in _selectedElements) {
                _elementConditionUpdates.Enqueue(new(ID,ElementConditonChange.SelectionRemoved));
            }
            ElementID oldSelectedElement = selectedElement.Value;
            SelectedElement = null;

            _elementConditionUpdates.Enqueue(new(oldSelectedElement,ElementConditonChange.SelectionRemoved));
        }

        protected void SetCapturedElement(ElementID elementID) {
            if(ConditionsPaused) {
                return;
            }
            ElementID? capturedElement = CapturedElement;
            if(capturedElement.HasValue) {
                _elementConditionUpdates.Enqueue(new(capturedElement.Value,ElementConditonChange.CaptureRemoved));
            }
            CapturedElement = elementID;

            _elementConditionUpdates.Enqueue(new(elementID,ElementConditonChange.CaptureAdded));
        }

        protected void ClearCapturedElement() {
            if(ConditionsPaused || !CapturedElement.HasValue) {
                return;
            }
            ElementID capturedElement = CapturedElement.Value;
            CapturedElement = null;

            _elementConditionUpdates.Enqueue(new(capturedElement,ElementConditonChange.CaptureRemoved));
        }

        private readonly Queue<ElementConditionUpdate> _elementConditionUpdates = new();

        public void PollCaptureStatusChanges() {
            while(_elementConditionUpdates.TryDequeue(out ElementConditionUpdate captureStatusUpdate)) {
                ApplyElementConditionChange(captureStatusUpdate.ID,captureStatusUpdate.Type);
            }
        }

        protected abstract void ApplyElementConditionChange(ElementID elementID,ElementConditonChange captureStatusChangeEvent);
    }
}
