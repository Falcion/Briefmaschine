using System.Diagnostics;

using Briefmaschine.Objects;
using Briefmaschine.Coroutines;

namespace Briefmaschine
{
    ///  <summary>
    /// Class containing basic constructors for instances of logger and its static methods
    /// </summary>
    public sealed class Logger
    {
        ///  <summary>
        /// Private string representing name of current logger's instace
        /// </summary>
        private readonly string? name;
        ///  <summary>
        /// Private string representing ID of current logger's instance
        /// </summary>
        private readonly string? id;

        ///  <summary>
        /// Static reference to instance of an logger's engine
        /// </summary>
        private static readonly Logger? instance = new Logger();

        ///  <summary>
        /// An const integer value representing frames count for stackfrace in instance of logging methods
        /// </summary>
        private const int STACKFRAMES = 2;

        ///  <summary>
        /// Nullable instance constructor for logger's engine
        /// </summary>
        public Logger(){}

        ///  <summary>
        /// Instance constructor of logger's engine
        /// </summary>
        ///  <param name="name">
        /// String representing the name of constructed instance
        /// </param>
        public Logger(string name)
        {
            this.name = name;
        }

        ///  <summary>
        /// Instance constructor of logger's engine
        /// </summary>
        ///  <param name="name">
        /// String representing the name of constructed instance
        /// </param>
        ///  <param name="id">
        /// String representing the ID of constructed instance
        /// </param>
        public Logger(string name, string id) : this(name)
        {
            this.id = id;
        }

        ///  <summary>
        /// Static method for displaying your entry within white color in console
        /// </summary>
        ///  <param name="message">
        /// String representing contents of future log's entry
        /// </param>
        ///  <param name="code_pos">
        /// String representing which section of the program log's entry belongs
        /// </param>
        ///  <param name="is_io">
        /// Boolean parameter representing will this entry be written in file
        /// </param>
        public static void Info(string message, string code_pos = "DEFAULT", bool is_io = false)
        {
            string datetime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

            string raw_message = $"/{code_pos}[STACKTRACE: PLACEHOLDER] {datetime} - {message}";

            Reconstructor(raw_message, is_io, Entries.INFO);
        }

        /// <summary>
        /// Static method for displaying your entry within yellow color in console
        /// </summary>
        ///  <param name="message">
        /// String representing contents of future log's entry
        /// </param>
        ///  <param name="code_pos">
        /// String representing which section of the program log's entry belongs
        /// </param>
        ///  <param name="is_io">
        /// Boolean parameter representing will this entry be written in file
        /// </param>
        public static void Warn(string message, string code_pos = "DEFAULT", bool is_io = false)
        {
            string datetime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

            string raw_message = $"/{code_pos}[STACKTRACE: PLACEHOLDER] {datetime} - {message}";

            Reconstructor(raw_message, is_io, Entries.WARN);
        }

        /// <summary>
        /// Static method for displaying your entry within green color in console
        /// </summary>
        ///  <param name="message">
        /// String representing contents of future log's entry
        /// </param>
        ///  <param name="code_pos">
        /// String representing which section of the program log's entry belongs
        /// </param>
        ///  <param name="is_io">
        /// Boolean parameter representing will this entry be written in file
        /// </param>
        public static void Success(string message, string code_pos = "DEFAULT", bool is_io = false)
        {
            string datetime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

            string raw_message = $"/{code_pos}[STACKTRACE: PLACEHOLDER] {datetime} - {message}";

            Reconstructor(raw_message, is_io, Entries.SUCCESS);
        }

        /// <summary>
        /// Static method for displaying your entry within red color in console
        /// </summary>
        ///  <param name="message">
        /// String representing contents of future log's entry
        /// </param>
        ///  <param name="code_pos">
        /// String representing which section of the program log's entry belongs
        /// </param>
        ///  <param name="is_io">
        /// Boolean parameter representing will this entry be written in file
        /// </param>
        public static void Error(string message, string code_pos = "DEFAULT", bool is_io = false)
        {
            string datetime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

            string raw_message = $"/{code_pos}[STACKTRACE: PLACEHOLDER] {datetime} - {message}";

            Reconstructor(raw_message, is_io, Entries.ERROR1);
        }

        ///  <summary>
        /// Private method of reconstructing and deploying entries of logger's methods
        /// </summary>
        ///  <param name="entry">
        /// String representing preprocessed log's entry out of method
        /// </param>
        ///  <param name="is_io">
        /// Boolean parameter representing will this entry be written in file
        /// </param>
        ///  <param name="entry_type">
        /// Enum representing type of logging method
        /// </param>
        private static void Reconstructor(string entry, bool is_io = false, Entries entry_type = Entries.INFO)
        {
            StackFrame stackframe = new(STACKFRAMES);

            var color = new ConsoleColor();

            string? method_target = string.Empty;

            if(stackframe != null)
            {
                method_target = stackframe.GetMethod()?.Name;

                if (method_target == null)
                    method_target = "UNKNOWN";
            }

            int index = entry.IndexOf("PLACEHOLDER");

            switch(entry_type)
            {
                case Entries.INFO:
                    if (index > -1)
                        entry = string.Concat("[INFO", entry.AsSpan(0, index), method_target, entry.AsSpan(index + 11));

                    color = ConsoleColor.White;
                    break;
                case Entries.WARN:
                    if (index > -1)
                        entry = string.Concat("[WARN", entry.AsSpan(0, index), method_target, entry.AsSpan(index + 11));

                    color = ConsoleColor.Yellow;
                    break;
                case Entries.ERROR1:
                    if (index > -1)
                        entry = string.Concat("[ERROR", entry.AsSpan(0, index), method_target, entry.AsSpan(index + 11));

                    color = ConsoleColor.Red;
                    break;
                case Entries.ERROR2:
                    if (index > -1)
                        entry = string.Concat("[CRASH", entry.AsSpan(0, index), method_target, entry.AsSpan(index + 11));

                    color = ConsoleColor.Red;
                    break;
                case Entries.SUCCESS:
                    if (index > -1)
                        entry = string.Concat("[SUCCESS", entry.AsSpan(0, index), method_target, entry.AsSpan(index + 11));

                    color = ConsoleColor.Green;
                    break;
            }

            Console.ForegroundColor = color;

            Console.Out.WriteLine(entry);

            Console.ResetColor();

            if(is_io)
            {
                string? path = Environment.GetEnvironmentVariable("LOGS_PATH");

                if (path == null)
                {
                    LIO.EnsureIO(string.Empty,
                                 string.Empty);

                    path = Environment.GetEnvironmentVariable("LOGS_PATH");

                    LIO.InjectIO(entry, path);
                }
                else
                    LIO.InjectIO(entry, path);
            }
        }

        ///  <summary>
        /// Custom method to display fully-customized log's entry
        /// </summary>
        ///  <param name = "message" >
        /// String representing contents of future log's entry
        /// </param>
        ///  <param name="code_pos">
        /// String representing which section of the program log's entry belongs
        /// </param>
        ///  <param name="custom_type">
        /// String representing type of logging methods alike of given perspective of instance
        /// </param>
        ///  <param name="colors">
        /// Pair parameter of console colors representing foreground color as first of the pair and background as second of the pair
        /// </param>
        ///  <param name="is_io">
        /// Boolean parameter representing will this entry be written in file
        /// </param>
        ///  <param name="path">
        /// String representing fullpath to future IO file of logs which in case of nullable, method replaces with ENV value
        /// </param>
        public static void Custom(string message, string? code_pos, string? custom_type, Pair<ConsoleColor>? colors, bool? is_io = false, string? path = null)
        {
            string datetime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

            StackFrame stackframe = new(STACKFRAMES);

            string? method_target = string.Empty;

            /* Here, we don't use "??=" operator for better readability:
             * current parameter is just like in stairs, has it's height
             */

            if (stackframe != null)
            {
                method_target = stackframe.GetMethod()?.Name;

                if (method_target == null)
                    method_target = "UNKNOWN";
            }

            if (code_pos == null)
                code_pos = "DEFAULT";

            if (custom_type == null)
                custom_type = "CUSTOM";

            if (colors == null)
                colors = new Pair<ConsoleColor>(ConsoleColor.White, 
                                                ConsoleColor.Black);

            if (is_io == null)
                is_io = false;

            string entry = $"[{custom_type}/{code_pos}][STACKTRACE: {method_target}] {datetime} - {message}";

            Console.ForegroundColor = colors.P1;
            Console.BackgroundColor = colors.P2;

            Console.Out.WriteLine(entry);

            Console.ResetColor();

            if(Convert.ToBoolean(is_io))
            {
                if(path != null)
                {
                    string? dest = Path.GetDirectoryName(path);

                    if (dest == string.Empty)
                        dest = ".audit";

                    string? file = Path.GetFileName(path);

                    if (file == string.Empty)
                        file = $"session-{DateTime.Now:dd-MM-yyyy}.logs";

                    /* Using "!" operator because IDE can't see overwriting to given instance */

                    LIO.EnsureIO(dest!, file!, false);

                    LIO.InjectIO(entry, path);
                }
                else
                {
                    string? keypath = Environment.GetEnvironmentVariable("LOGS_PATH");

                    if (keypath == null) LIO.EnsureIO();

                    keypath = Environment.GetEnvironmentVariable("LOGS_PATH");

                    LIO.InjectIO(entry, keypath);
                }
            }

        }

        ///  <summary>
        /// String representing name of current logger's instace
        /// </summary>
        public string? Name { get { return name; } }
        ///  <summary>
        /// String representing ID of current logger's instace
        /// </summary>
        public string? ID { get { return id; } }

        ///  <summary>
        /// Static reference to instance of an logger's engine
        /// </summary>
        public Logger? Instance { get { return instance; } }
    }
}
