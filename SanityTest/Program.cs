namespace SanityTest {

public struct BrokenValueType {
   //public static BrokenValueType A = default; /* Okay! */
    public static BrokenValueType B = A; /* Also okay! */
    public static BrokenValueType C = D, D = C; /* Also... completely fine. */

    public (BrokenValueType A, int B) AB { get; init; } /* Can't compile, CS0525 */

    public static (BrokenValueType A, int B) AB = (default, 0); /* Bad! 'System.TypeLoadException' at first consumer of type. */
}

    public static class Program {
        public static void Main(string[] args) {
            var value = new BrokenValueType(); // Crashes..
            Console.ReadKey(true);
        }
    }
}
