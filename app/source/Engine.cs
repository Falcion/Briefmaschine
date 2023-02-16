using System.Diagnostics;
using System.Text;

namespace Briefmaschine
{
    public static class Engine
    {
        private const ushort FRAMES_STACK = 2;

        private enum Logs
        {
            INFO,
            WARN,
            ERROR1,
            ERROR2,
            SUCCESS
        }

        public static void Info(string message, string code_pos = "DEFAULT", bool is_io = false)
        {
            string prebuilded_entry = $"/{code_pos}][STACKTRACE: _A11DUMP] {DateTime.Now:dd.MM.yyyy HH:mm:ss} - {message}";

            LOGGER_CONSTRUCT(prebuilded_entry, is_io, Logs.INFO);
        }

        public static void Warn(string message, string code_pos = "DEFAULT", bool is_io = false)
        {
            string prebuilded_entry = $"/{code_pos}][STACKTRACE: _A11DUMP] {DateTime.Now:dd.MM.yyyy HH:mm:ss} - {message}";

            LOGGER_CONSTRUCT(prebuilded_entry, is_io, Logs.WARN);
        }

        public static void Error(string message, string? code_pos = "DEFAULT", bool is_io = false)
        {
            string prebuilded_entry = $"/{code_pos}][STACKTRACE: _A11DUMP] {DateTime.Now:dd.MM.yyyy HH:mm:ss} - {message}";

            LOGGER_CONSTRUCT(prebuilded_entry, is_io, Logs.ERROR1);
        }

        public static void Error(Exception exception, bool is_io = true, int refer = int.MaxValue)
        {
            string message = exception.Message;

            var targetsite = exception.TargetSite;

            string? _target = string.Empty;

            if (targetsite == null)
                _target = targetsite?.Name;

            _target ??= "UNDEFINED";

            string prebuilded_entry = $"/APP-{refer}][STACKTRACE: _A11DUMP][TARGET: {_target}] {DateTime.Now:dd.MM.yyyy HH:mm:ss} - {message}";

            LOGGER_CONSTRUCT(prebuilded_entry, is_io, Logs.ERROR2);
        }

        public static void Success(string message, string code_pos = "DEFAULT", bool is_io = false)
        {
            string prebuilded_entry = $"/{code_pos}][STACKTRACE: _A11DUMP] {DateTime.Now:dd.MM.yyyy HH:mm:ss} - {message}";

            LOGGER_CONSTRUCT(prebuilded_entry, is_io, Logs.SUCCESS);
        }

        private static void LOGGER_CONSTRUCT(string entry, bool is_io = false, Logs type = Logs.INFO)
        {
            StackFrame? stackframe = new(FRAMES_STACK);

            string? stacktrace = null;

            if (stackframe != null)
                stacktrace = stackframe.GetMethod()?.Name;

            stacktrace ??= "UNDEFINED";

            dynamic predefined_color;

            switch (type)
            {
                case Logs.INFO:
                    predefined_color = ConsoleColor.White;

                    entry = "[INFO" + entry.Replace("_A11DUMP", stacktrace);
                    break;
                case Logs.WARN:
                    predefined_color = ConsoleColor.Yellow;

                    entry = "[WARN" + entry.Replace("_A11DUMP", stacktrace);
                    break;
                case Logs.ERROR1:
                    predefined_color = ConsoleColor.Red;

                    entry = "[ERRORPOINT" + entry.Replace("_A11DUMP", stacktrace);
                    break;
                case Logs.ERROR2:
                    predefined_color = ConsoleColor.DarkRed;

                    entry = "[BREAKPOINT" + entry.Replace("_A11DUMP", stacktrace);
                    break;
                case Logs.SUCCESS:
                    predefined_color = ConsoleColor.Green;

                    entry = "[SUCCESS" + entry.Replace("_A11DUMP", stacktrace);
                    break;
                default:
                    predefined_color = ConsoleColor.Gray;

                    entry = "[UNKNOWN" + entry.Replace("_A11DUMP", stacktrace);
                    break;
            }

            if (is_io)
            {
                string? path = Environment.GetEnvironmentVariable("LOGS_PATH");

                if (path == null)
                {
                    IO.EnsureIO(null, null);

                    path = Environment.GetEnvironmentVariable("LOGS_PATH");

                    IO.UpdateIO(entry, path);
                }
                else
                    IO.UpdateIO(entry, path);
            }

            Console.ForegroundColor = predefined_color;

            Console.Out.WriteLine(entry);

            Console.ResetColor();
        }
        
        public static class IO
        {
            private const string PATH_EXCEPTION_MESSAGE = "Via parsing and trying to contain path's variable (directory or filename) of update's method exception was made.";

            public static void EnsureIO(string? dest = ".audit", string? filename = "", bool is_env = true)
            {
                if (dest == null || dest == "")
                    dest = ".audit";

                if (filename == null || filename == "")
                    filename = $"session-{DateTime.Now:dd-MM-yyyy}.logs";

                string path = Path.Combine(dest, filename);

                if(!Directory.Exists(dest))
                    Directory.CreateDirectory(dest);

                if(!File.Exists(path))
                    using(var stream = File.Create(path))
                    {
                        byte[] message = Encoding.UTF8.GetBytes($"[INFO/INIT][STACKFRAME: UNDEFINED] | {DateTime.Now:dd.MM.yyyy HH:mm:ss} - Created first instance for logs!" + "\n");

                        stream.Write(message);
                        stream.Close();
                    }

                if (is_env)
                    Environment.SetEnvironmentVariable("LOGS_PATH", path);
            }

            public static void UpdateIO(string message, string? path)
            {
                /* Instead of a custom logging method, we need to find a file for IO's future work: we 
                 * throw exceptions because this is endpoint code without user intervention.
                 */

                if (path != null)
                {
                    string? dest = Path.GetDirectoryName(path);

                    if (dest == null || dest == "")
                        throw new ArgumentNullException(nameof(path), PATH_EXCEPTION_MESSAGE);

                    string? filename = Path.GetFileName(path);

                    if (filename == null || filename == "")
                        throw new ArgumentNullException(nameof(path), PATH_EXCEPTION_MESSAGE);

                    EnsureIO(dest, filename);
                }
                else
                {
                    /* Double the value of the ENV variable to escape the nullable value.
                     */

                    string? LOGS_PATH = Environment.GetEnvironmentVariable("LOGS_PATH");

                    if (LOGS_PATH == null) EnsureIO();
                }

                path = Environment.GetEnvironmentVariable("LOGS_PATH");

                using var writer = File.AppendText(path!);

                writer.Write(message + "\n");
                writer.Close();
            }
        }

        public static class Custom
        {
            public static void Log(string message, string? code_pos, string? type, Colors? colors, bool is_io = false, string? path = null)
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

                code_pos ??= "DEFAULT";

                type ??= "CUSTOM";

                colors ??= new Colors();

                string entry = $"[{type}/{code_pos}][STACKTRACE: {stacktrace}] {DateTime.Now:dd.MM.yyyy HH:mm:ss} - {message}";

                /* Here we use similar but different IO's in this custom logging update method, but we
                 * can't throw exceptions and need to provide some context due to the end task of the code.
                 */

                if (is_io)
                {
                    if (path != null)
                    {
                        string? dest = Path.GetDirectoryName(path);

                        if (dest == null || dest == "")
                            dest = ".audit";

                        string? filename = Path.GetFileName(path);

                        if (filename == null || filename == "")
                            filename = $"session-{DateTime.Now:dd-MM-yyyy}.logs";

                        IO.EnsureIO(dest, filename, false);

                        IO.UpdateIO(entry, path);
                    }
                    else
                    {
                        string? LOGS_PATH = Environment.GetEnvironmentVariable("LOGS_PATH");

                        if (LOGS_PATH == null) IO.EnsureIO();

                        LOGS_PATH = Environment.GetEnvironmentVariable("LOGS_PATH")!;

                        IO.UpdateIO(entry, LOGS_PATH);
                    }
                }

                Console.ForegroundColor = colors.Foreground;
                Console.BackgroundColor = colors.Background;

                Console.Out.WriteLine(entry);

                Console.ResetColor();
            }
        }
    }

    public class Colors
    {
        private readonly ConsoleColor foreground;
        private readonly ConsoleColor background;

        public Colors(ConsoleColor foreground, ConsoleColor background)
        {
            this.foreground = foreground;
            this.background = background;
        }

        public Colors(ConsoleColor foreground)
        {
            this.foreground = foreground;

            this.background = Console.BackgroundColor;
        }

        public Colors()
        {
            this.foreground = Console.ForegroundColor;
            this.background = Console.BackgroundColor;
        }

        public ConsoleColor Foreground { get { return this.foreground; } }
        public ConsoleColor Background { get { return this.background; } }
    }
}
