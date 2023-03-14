namespace Briefmaschine.Objects
{
    ///  <summary>
    /// Object representing a pair of generic type parameters
    /// </summary>
    ///  <typeparam name="T">
    /// Generic type-parameter representing type of pair's values
    /// </typeparam>
    public class Pair<T>
    {
        ///  <summary>
        /// Generic value representing first parameter of the pair
        /// </summary>
        public T? P1 { get; set; }
        ///  <summary>
        /// Generic value representing second parameter of the pair
        /// </summary>
        public T? P2 { get; set; }

        ///  <summary>
        /// Nullable instance constructor for pairs
        /// </summary>
        public Pair(){}

        ///  <summary>
        /// Instance constructor for pairs
        /// </summary>
        ///  <param name="P1">
        /// Generic representing first parameter of the pair's instance
        /// </param>
        ///  <param name="P2">
        /// Generic representing second parameter of the pair's instance
        /// </param>
        public Pair(T P1, T P2)
        {
            this.P1 = P1;
            this.P2 = P2;
        }
    }
}
