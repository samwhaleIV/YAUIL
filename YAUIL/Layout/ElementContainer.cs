using System.Runtime.Serialization;

namespace YAUIL.Layout {
    /// <remarks>
    /// Not thread safe.
    /// </remarks>
    public sealed class ElementContainer {

        private readonly Dictionary<ElementID,Element> _elements;

        public Area Area { get; set; }

        public ElementContainer(int maxElementCount = Constants.DefaultMaxElementCount) {
            _maxElementCount = maxElementCount;
            _elementOutputBuffer = new ElementOutput[_maxElementCount];
            _elements = new(maxElementCount);
            _elementsInOutputBuffer = new(maxElementCount);
            _circularReferenceHitList = new(maxElementCount);
        }

        private readonly int _maxElementCount;
        public int MaxElementCount => _maxElementCount;

        private readonly ElementOutput[] _elementOutputBuffer;
        private readonly Dictionary<ElementID,int> _elementsInOutputBuffer;

        private readonly HashSet<ElementID> _circularReferenceHitList;

        private int _outputBufferIndex = 0;

        private static bool HasParent(Element element) => element.Parent != ElementID.None;

        private Area GetParentArea(Element element) {
            if(!HasParent(element) || !_elementsInOutputBuffer.TryGetValue(element.Parent,out int bufferIndex)) {
                return Area;
            }
            return _elementOutputBuffer[bufferIndex].Area;
        }

        private void AddElementToOutputBuffer(Element element) {
            if(HasParent(element) && !_elementsInOutputBuffer.ContainsKey(element.Parent) && _elements.TryGetValue(element.Parent,out Element parentElement)) {
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

        private void AssertNoCircularReferences(Element element) {
            ElementID startID = element.ID;
            while(HasParent(element) && _elements.TryGetValue(element.Parent,out element)) {
                if(HasParent(element) && element.Parent == startID || _circularReferenceHitList.Contains(element.ID)) {
                    throw new ElementContainerException($"Circular reference detected in parent chain.");
                }
                _circularReferenceHitList.Add(element.ID);
            }
        }

        public void Add(Element element) {
            if(element.ID == ElementID.None) {
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

        public void Remove(ElementID ID) {
            if(!_elements.ContainsKey(ID)) {
                throw new ElementContainerException($"Cannot remove element with ID '{ID}', it is not currently in use by the container.");
            }
            _elements.Remove(ID);
        }

        public bool HasElement(ElementID ID) {
            return _elements.ContainsKey(ID);
        }

        public void Clear() {
            _elements.Clear();
        }
    }
}
