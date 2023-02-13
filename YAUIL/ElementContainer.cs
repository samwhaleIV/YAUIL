namespace YAUIL {
    /// <remarks>
    /// Not thread safe.
    /// </remarks>
    public sealed class ElementContainer {

        private Dictionary<ulong,Element> _elements;

        private const int DEFAULT_MAX_ELEMENT_COUNT = 64;

        public Area Area { get; set; }

        public ElementContainer(int maxElementCount = DEFAULT_MAX_ELEMENT_COUNT) {
            _maxElementCount = maxElementCount;
            _elementOutputBuffer = new ElementOutput[_maxElementCount];
            _elements = new(maxElementCount);
            _elementsInOutputBuffer = new(maxElementCount);
            _circularReferenceHitList = new(maxElementCount);
        }

        private readonly int _maxElementCount;
        public int MaxElementCount => _maxElementCount;

        private readonly ElementOutput[] _elementOutputBuffer;
        private readonly Dictionary<ulong,int> _elementsInOutputBuffer;

        private int _outputBufferIndex = 0;

        private static bool HasParent(Element element) => element.ParentID != uint.MinValue;

        private Area GetParentArea(Element element) {
            if(!HasParent(element) || !_elementsInOutputBuffer.TryGetValue(element.ParentID,out int bufferIndex)) {
                return Area;
            }
            return _elementOutputBuffer[bufferIndex].Area;
        }

        private void AddElementToOutputBuffer(Element element) {
            if(HasParent(element) && !_elementsInOutputBuffer.ContainsKey(element.ParentID) && _elements.TryGetValue(element.ParentID,out Element parentElement)) {
                AddElementToOutputBuffer(parentElement); /* Warning: Recursive */
            }
       
            ElementOutput elementOutput = TranslationHelper.GetElementOutput(element,Area,GetParentArea(element));

            int outputBufferIndex = _outputBufferIndex;
            _outputBufferIndex += 1;

            _elementOutputBuffer[outputBufferIndex] = elementOutput;
            _elementsInOutputBuffer.Add(element.ID,outputBufferIndex);
        }

        private void UpdateElementOutputBuffer() {
            _elementsInOutputBuffer.Clear();
            _outputBufferIndex = 0;
            foreach(var element in _elements.Values) {
                if(_elementsInOutputBuffer.ContainsKey(element.ID)) {
                    continue;
                }
                AddElementToOutputBuffer(element);  
            }
        }

        public ReadOnlySpan<ElementOutput> GetLayout() {
            UpdateElementOutputBuffer();
            return new ReadOnlySpan<ElementOutput>(_elementOutputBuffer,0,_elements.Count);
        }

        private readonly HashSet<ulong> _circularReferenceHitList;

        private void AssertNoCircularReferences(Element element) {
            ulong startID = element.ID;
            while(HasParent(element) && _elements.TryGetValue(element.ParentID,out element)) {
                if(HasParent(element) && element.ParentID == startID || _circularReferenceHitList.Contains(element.ID)) {
                    throw new ElementContainerException($"Circular reference detected in parent chain.");
                }
                _circularReferenceHitList.Add(element.ID);
            }
        }

        public void Add(Element element) {
            if(element.ID == uint.MinValue) {
                throw new ElementContainerException($"Element ID cannot be {uint.MinValue}.");
            }
            if(_elements.ContainsKey(element.ID)) {
                throw new ElementContainerException($"ID '{element.ID}' is already in use.");
            }
            if(_elements.Count >= MaxElementCount) {
                throw new ElementContainerException($"Cannot add element with ID '{element.ID}', element container will overflow. The container's max size is {MaxElementCount}.");
            }
            _circularReferenceHitList.Clear();
            AssertNoCircularReferences(element);
            _elements.Add(element.ID,element);
        }

        public void Remove(ulong ID) {
            if(!_elements.ContainsKey(ID)) {
                throw new ElementContainerException($"Cannot remove element with ID '{ID}', it is not currently in use by the container.");
            }
            _elements.Remove(ID);
        }

        public void Remove(Element element) => Remove(element.ID);
    }
}
