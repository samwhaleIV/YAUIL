using System.Runtime.Serialization;

namespace YAUIL.Layout {
	[Serializable]
	public class ElementContainerException:Exception {
		public ElementContainerException() { }
		public ElementContainerException(string message) : base(message) { }
		public ElementContainerException(string message,Exception inner) : base(message,inner) { }
		protected ElementContainerException(SerializationInfo info,StreamingContext context) : base(info,context) { }
	}
}
