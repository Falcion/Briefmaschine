using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briefsmaschine
{
    public static class Objects
    {
        public class ColorsPair
        {
            public readonly ConsoleColor ForegroundColor;
            public readonly ConsoleColor BackgroundColor;

            public ColorsPair(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
            {
                ForegroundColor = foregroundColor;
                BackgroundColor = backgroundColor;
            }

            public ColorsPair(ConsoleColor foregroundColor)
            {
                ForegroundColor = foregroundColor;
                BackgroundColor = ConsoleColor.Black;
            }

            public ColorsPair()
            {
                ForegroundColor = ConsoleColor.Gray;
                BackgroundColor = ConsoleColor.Black;
            }
        }
    }
}
