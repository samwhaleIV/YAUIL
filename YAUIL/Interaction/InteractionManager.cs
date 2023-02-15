using YAUIL.Layout;

namespace YAUIL.Interaction {
    public abstract class InteractionManager:ElementConditioner {

        private readonly ElementContainer _elementContainer;
        public ElementContainer ElementContainer => _elementContainer;

        public InteractionManager(ElementContainer elementContainer) {
            _elementContainer = elementContainer;
        }

        private readonly Queue<ImpulseEvent> _eventQueue = new();

        public void QueueEvent(ImpulseEvent impulseEvent) {
            _eventQueue.Enqueue(impulseEvent);
        }

        public void PollEventQueue() {
            while(_eventQueue.TryDequeue(out ImpulseEvent impulseEvent)) {
                ProcessEvent(impulseEvent);
            }
        }

        private void ProcessEvent(ImpulseEvent impulseEvent) {
            throw new NotImplementedException();
        }
    }
}
