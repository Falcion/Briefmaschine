namespace Briefmaschine.Objects.Tupples
{
    /// <summary>
    /// Object representing a trupple of generic type parameters
    /// </summary>
    /// <typeparam name="T">
    /// Generic type-parameter representing type of trupple's values
    /// </typeparam>
    public class Trupple<T>
    {
        /// <summary>
        /// Generic value representing first parameter of the trupple
        /// </summary>
        public T? P1 { get; set; }

        /// <summary>
        /// Generic value representing second parameter of the trupple
        /// </summary>
        public T? P2 { get; set; }

        /// <summary>
        /// Generic value representing third parameter of the trupple
        /// </summary>
        public T? P3 { get; set; }

        /// <summary>
        /// Nullable instance constructor for trupples
        /// </summary>
        public Trupple() { }

        /// <summary>
        /// Instance constructor for trupples
        /// </summary>
        /// <param name="P1">
        /// Generic representing first parameter of the trupple's instance
        /// </param>
        /// <param name="P2">
        /// Generic representing second parameter of the trupple's instance
        /// </param>
        /// <param name="P3">
        /// Generic representing third parameter of the trupple's instance
        /// </param>
        public Trupple(T P1, T P2, T P3)
        {
            this.P1 = P1;
            this.P2 = P2;
            this.P3 = P3;
        }
    }
}
