namespace Briefmaschine.Objects.Multiples
{
    /// <summary>
    /// Object representing a single of generic type parameter
    /// </summary>
    /// <typeparam name="T">
    /// Generic type-parameter representing type of single's value
    /// </typeparam>
    public class Single<T>
    {
        /// <summary>
        /// Generic value representing parameter of the single
        /// </summary>
        public T? P { get; set; }

        /// <summary>
        /// Nullable instance constructor for singles
        /// </summary>
        public Single() { }

        /// <summary>
        /// Instance constructor for singles
        /// </summary>
        /// <param name="P">
        /// Generic representing parameter of the single's instance
        /// </param>
        public Single(T P)
        {
            this.P = P;
        }
    }
}
