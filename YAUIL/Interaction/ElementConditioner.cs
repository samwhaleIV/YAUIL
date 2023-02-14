namespace YAUIL.Interaction {
    public abstract class ElementConditioner {

        private readonly HashSet<ElementID> _selectedElements = new();
        private readonly Queue<ElementConditionUpdate> _elementConditionUpdates = new();

        public IEnumerable<ElementID> GetSelectedElements() => _selectedElements;
        public int SelectedElementCount => _selectedElements.Count;

        public ElementID? CapturedElement { get; private set; } = null;
        public ElementID? SelectedElement { get; private set; } = null;

        protected void AddSelectedElement(ElementID elementID) {
            if(_selectedElements.Contains(elementID)) {
                return;
            }
            SelectedElement = elementID;    
            _selectedElements.Add(elementID);
            _elementConditionUpdates.Enqueue(new(elementID,ElementConditonChange.SelectionAdded));
        }

        protected void ClearSelectedElements() {
            ElementID? selectedElement = SelectedElement;
            if(!selectedElement.HasValue) {
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
            ElementID? capturedElement = CapturedElement;
            if(capturedElement.HasValue) {
                _elementConditionUpdates.Enqueue(new(capturedElement.Value,ElementConditonChange.CaptureRemoved));
            }
            CapturedElement = elementID;

            _elementConditionUpdates.Enqueue(new(elementID,ElementConditonChange.CaptureAdded));
        }

        protected void ClearCapturedElement() {
            if(!CapturedElement.HasValue) {
                return;
            }
            ElementID capturedElement = CapturedElement.Value;
            CapturedElement = null;

            _elementConditionUpdates.Enqueue(new(capturedElement,ElementConditonChange.CaptureRemoved));
        }

        public void PollElementConditionChanges() {
            while(_elementConditionUpdates.TryDequeue(out ElementConditionUpdate elementConditionUpdate)) {
                ApplyElementConditionChange(elementConditionUpdate.ID,elementConditionUpdate.Type);
            }
        }

        protected abstract void ApplyElementConditionChange(ElementID elementID,ElementConditonChange captureStatusChangeEvent);
    }
}
