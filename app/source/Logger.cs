using Briefmaschine.Objects;
using System.Diagnostics;
using System.Reflection;

namespace Briefmaschine
{
    /// <summary>
    /// Logger representation which contains different logging methods and connection to <see cref="LoggerIo"/>
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Public tatic instance of <see cref="Logger"/> class 
        /// </summary>
        public static Logger Instance { get; set; }

        /// <summary>
        /// Enum representing different types of logging methods
        /// </summary>
        private enum EntryType
        {
            INFO,
            WARN,
            ERROR1,
            ERROR2,
            SUCCESS
        }

        /// <summary>
        /// Const for <see cref="StackFrame"/> frames count for stacktrace in logging methods
        /// </summary>
        private const ushort STACKFRAMES = 2;

        /// <summary>
        /// Writes an information message with white color into the <see cref="Console"/>
        /// </summary>
        /// <param name="message">
        /// A string representing your message which will be logged
        /// </param>
        /// <param name="codePos">
        /// A string representing a category in which event happens
        /// </param>
        /// <param name="isIo">
        /// Boolean parameter defining will be your message written into logs session file or not
        /// </param>
        public static void Info(string message, string codePos = "DEFAULT", bool isIo = false)
        {
            string rawEntry = $"/{codePos}][STACKTRACE: 0b5385ca] {DateTime.Now:dd.MM.yyyy HH:mm:ss} - {message}";

            LogsConstructor(rawEntry, isIo, EntryType.INFO);
        }

        /// <summary>
        /// Writes an warning message with yellow color into the <see cref="Console"/>
        /// </summary>
        /// <param name="message">
        /// A string representing your message which will be logged
        /// </param>
        /// <param name="codePos">
        /// A string representing a category in which event happens
        /// </param>
        /// <param name="isIo">
        /// Boolean parameter defining will be your message written into logs session file or not
        /// </param>
        public static void Warn(string message, string codePos = "DEFAULT", bool isIo = false)
        {
            string rawEntry = $"/{codePos}][STACKTRACE: 0b5385ca] {DateTime.Now:dd.MM.yyyy HH:mm:ss} - {message}";

            LogsConstructor(rawEntry, isIo, EntryType.WARN);
        }

        /// <summary>
        /// Writes an error/exception message with red color into the <see cref="Console"/>
        /// </summary>
        /// <param name="message">
        /// A string representing your message which will be logged
        /// </param>
        /// <param name="codePos">
        /// A string representing a category in which event happens
        /// </param>
        /// <param name="isIo">
        /// Boolean parameter defining will be your message written into logs session file or not
        /// </param>
        public static void Error(string message, string? codePos = "DEFAULT", bool isIo = false)
        {
            string rawEntry = $"/{codePos}][STACKTRACE: 0b5385ca] {DateTime.Now:dd.MM.yyyy HH:mm:ss} - {message}";

            LogsConstructor(rawEntry, isIo, EntryType.ERROR1);
        }

        /// <summary>
        /// Injects information from given <see cref="Exception"/> and retrieves it into red message
        /// </summary>
        /// <param name="exception">
        /// <see cref="Exception"/> which occured and need to be messages by "butchering" it
        /// </param>
        /// <param name="isIo">
        /// Boolean parameter defining will be your message written into logs session file or not</param>
        /// <param name="refer">
        /// An integer number representing a context or hash code for instance of an <paramref name="exception"/>
        /// </param>
        public static void Error(Exception exception, bool isIo = true, int refer = int.MaxValue)
        {
            string message = exception.Message;

            MethodBase? targetsite = exception.TargetSite;

            string? targetname = string.Empty;

            if (targetsite == null)
                targetname = targetsite?.Name;

            targetname ??= "UNDEFINED";

            string rawEntry = $"/APP-{refer}][STACKTRACE: 0b5385ca][TARGET: {targetname}] {DateTime.Now:dd.MM.yyyy HH:mm:ss} - {message}";

            LogsConstructor(rawEntry, isIo, EntryType.ERROR2);
        }

        /// <summary>
        /// Writes an success message with green color into the <see cref="Console"/>
        /// </summary>
        /// <param name="message">
        /// A string representing your message which will be logged
        /// </param>
        /// <param name="codePos">
        /// A string representing a category in which event happens
        /// </param>
        /// <param name="isIo">
        /// Boolean parameter defining will be your message written into logs session file or not
        /// </param>
        public static void Success(string message, string codePos = "DEFAULT", bool isIo = false)
        {
            string rawEntry = $"/{codePos}][STACKTRACE: 0b5385ca] {DateTime.Now:dd.MM.yyyy HH:mm:ss} - {message}";

            LogsConstructor(rawEntry, isIo, EntryType.SUCCESS);
        }

        /// <summary>
        /// Method of log's method constructor with given context into <see cref="Console"/> and <see cref="File"/>
        /// </summary>
        /// <param name="entry">
        /// String representing message of future log-event
        /// </param>
        /// <param name="isIo">
        /// Boolean parameter defining will be your message written into logs session file or not
        /// </param>
        /// <param name="type">
        /// A value of <see cref="EntryType"/> defining type of log-event
        /// </param>
        private static void LogsConstructor(string entry, bool isIo = false, EntryType type = EntryType.INFO)
        {
            StackFrame? stackframe = new(STACKFRAMES);

            string? stacktrace = null;

            if (stackframe != null)
                stacktrace = stackframe.GetMethod()?.Name;

            stacktrace ??= "UNDEFINED";

            ConsoleColor messageColor;

            int index = entry.IndexOf("0b5385ca");

            switch (type)
            {
                case EntryType.INFO:
                    messageColor = ConsoleColor.White;

                    if (index > -1)
                        entry = "[INFO" + entry.Substring(0, index) + stacktrace + entry.Substring(index + "0b5385ca".Length);
                    break;
                case EntryType.WARN:
                    messageColor = ConsoleColor.Yellow;

                    if (index > -1)
                        entry = "[WARN" + entry.Substring(0, index) + stacktrace + entry.Substring(index + "0b5385ca".Length);
                    break;
                case EntryType.ERROR1:
                    messageColor = ConsoleColor.Red;

                    if (index > -1)
                        entry = "[ERROR" + entry.Substring(0, index) + stacktrace + entry.Substring(index + "0b5385ca".Length);
                    break;
                case EntryType.ERROR2:
                    messageColor = ConsoleColor.DarkRed;

                    if (index > -1)
                        entry = "[CRASH" + entry.Substring(0, index) + stacktrace + entry.Substring(index + "0b5385ca".Length);
                    break;
                case EntryType.SUCCESS:
                    messageColor = ConsoleColor.Green;

                    if (index > -1)
                        entry = "[SUCCESS" + entry.Substring(0, index) + stacktrace + entry.Substring(index + "0b5385ca".Length);
                    break;
                default:
                    messageColor = ConsoleColor.Gray;

                    if (index > -1)
                        entry = "[UNKNOWN" + entry.Substring(0, index) + stacktrace + entry.Substring(index + "0b5385ca".Length);
                    break;
            }

            if (isIo)
            {
                string? path = Environment.GetEnvironmentVariable("LOGS_PATH");

                if (path == null)
                {
                    LoggerIo.EnsureIo(null, null);

                    path = Environment.GetEnvironmentVariable("LOGS_PATH");

                    LoggerIo.UpdateIo(entry, path);
                }
                else
                    LoggerIo.UpdateIo(entry, path);
            }

            Console.ForegroundColor = messageColor;

            Console.Out.WriteLine(entry);

            Console.ResetColor();
        }

        /// <summary>
        /// Method with full-custom parameters support including colors
        /// </summary>
        /// <param name="message">
        /// String representing message of future log-event
        /// </param>
        /// <param name="codePos">
        /// A string representing a category in which event happens
        /// </param>
        /// <param name="type">
        /// A value of string type defining log-event simillar with <see cref="EntryType"/>
        /// </param>
        /// <param name="colors">
        /// Instance of <see cref="ConsoleColorPair"/> which defines foreground and background colors of <see cref="Console"/>
        /// </param>
        /// <param name="isIo">
        /// Boolean parameter defining will be your message written into logs session file or not
        /// </param>
        /// <param name="path">
        /// Custom full-defined path to logs session file with dirpath
        /// </param>
        public static void CustomLog(string message, string? codePos, string? type, ConsoleColorPair? colors, bool isIo = false, string? path = null)
        {
            /* Here, because the constructor is called directly by the code and not by essentials, we 
             * use a different number of frames.
             */

            StackFrame? stackframe = new(1);

            string? stacktrace = null;

            if (stackframe != null)
                stacktrace = stackframe.GetMethod()?.Name;

            /* Use null-coasceling operators to improve readability, and declare first-instance 
             * constructs when entering audit messages.
             */

            stacktrace ??= "UNDEFINED";

            codePos ??= "DEFAULT";

            type ??= "CUSTOM";

            colors ??= new ConsoleColorPair();

            string entry = $"[{type}/{codePos}][STACKTRACE: {stacktrace}] {DateTime.Now:dd.MM.yyyy HH:mm:ss} - {message}";

            /* Here we use similar but different IO's in this custom logging update method, but we
             * can't throw exceptions and need to provide some context due to the end task of the code.
             */

            if (isIo)
            {
                if (path != null)
                {
                    string? dest = Path.GetDirectoryName(path);

                    if (dest == null || dest == "")
                        dest = ".audit";

                    string? filename = Path.GetFileName(path);

                    if (filename == null || filename == "")
                        filename = $"session-{DateTime.Now:dd-MM-yyyy}.logs";

                    LoggerIo.EnsureIo(dest, filename, false);

                    LoggerIo.UpdateIo(entry, path);
                }
                else
                {
                    string? pathEnv = Environment.GetEnvironmentVariable("LOGS_PATH");

                    if (pathEnv == null) LoggerIo.EnsureIo();

                    pathEnv = Environment.GetEnvironmentVariable("LOGS_PATH")!;

                    LoggerIo.UpdateIo(entry, pathEnv);
                }
            }

            Console.ForegroundColor = colors.ForegroundColor;
            Console.BackgroundColor = colors.BackgroundColor;

            Console.Out.WriteLine(entry);

            Console.ResetColor();
        }
    }
}
