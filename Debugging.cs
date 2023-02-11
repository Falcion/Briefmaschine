using Briefmaschine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Briefmaschine.Kern;
using static Briefmaschine.Kern.Customs;

namespace Briefsmaschine
{
    internal class Debugging
    {
        public static void Main(string[] args)
        {
            Info("INFO-DEBUG", "1", true);
            Warn("WARN-DEBUG");
            Success("SUCCESS-DEBUG");
            Error("ERROR-DEBUG");

            var colors = new Objects.ColorsPair(ConsoleColor.Magenta);

            Customs.Log("CUSTOM", "!", "DEFAULT", colors);

            Log("CUSTOMFS", "FS", "!", colors, true, "custom", "default.logs");
        }
    }
}
