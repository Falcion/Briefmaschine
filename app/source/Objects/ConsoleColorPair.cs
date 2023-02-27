namespace Briefmaschine.Objects
{
    /// <summary>
    /// Custom instance of pair of <see cref="ConsoleColorPair"/> for foreground and background colors
    /// </summary>
    public sealed class ConsoleColorPair
    {
        /// <summary>
        /// A value of <see cref="ConsoleColor"/> which represents foreground color of <see cref="Console"/>
        /// </summary>
        public ConsoleColor ForegroundColor { get; set; }
        /// <summary>
        /// A value of <see cref="ConsoleColor"/> which represents background color of <see cref="Console"/>
        /// </summary>
        public ConsoleColor BackgroundColor { get; set; }

        /// <summary>
        /// Instance-constructor for <see cref="ConsoleColorPair"/> with pair of <see cref="ConsoleColor"/>
        /// </summary>
        /// <param name="foregroundColor">
        /// A value of <see cref="ConsoleColor"/> which represents foreground color of <see cref="Console"/>
        /// </param>
        /// <param name="backgroundColor">
        /// A value of <see cref="ConsoleColor"/> which represents background color of <see cref="Console"/>
        /// </param>
        public ConsoleColorPair(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        /// <summary>
        /// Instance-constructor for <see cref="ConsoleColorPair"/> with <see cref="ConsoleColor"/> of foreground
        /// </summary>
        /// <param name="foregroundColor">
        /// A value of <see cref="ConsoleColor"/> which represents foreground color of <see cref="Console"/>
        /// </param>
        public ConsoleColorPair(ConsoleColor foregroundColor)
        {
            ForegroundColor = foregroundColor;

            BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// Instance-constructor for <see cref="ConsoleColorPair"/> with no-arguments
        /// </summary>
        public ConsoleColorPair()
        {
            ForegroundColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;
        }
    }
}
