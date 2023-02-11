using Briefsmaschine;
using System.Diagnostics;
using System.Text;
using static Briefsmaschine.Objects;

namespace Briefmaschine
{
    public static class Kern
    {
        public static void Info(string message, string code_pos = "DEFAULT", bool is_io = false)
        {
            var stackframe = new StackFrame(1);

            string? methodName = stackframe.GetMethod()?.Name;

            string datetime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

            string logEntry = $"[INFO/{code_pos.ToUpper()}] < STACKFRAME: {(methodName == null ? "UNDEFINED" : methodName.ToUpper())} > | {datetime} - {message}";

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            Console.Out.WriteLine(logEntry);

            Console.ResetColor();

            if(is_io)
            {
                Profiler.EnsureLogs();
                string? path = Environment.GetEnvironmentVariable("LOGS_PATH");
                Profiler.UpdateLogs(logEntry, path);
            }
        }

        public static void Warn(string message, string code_pos = "DEFAULT", bool is_io = false)
        {
            var stackframe = new StackFrame(1);

            string? methodName = stackframe.GetMethod()?.Name;

            string datetime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

            string logEntry = $"[WARN/{code_pos.ToUpper()}] < STACKFRAME: {(methodName == null ? "UNDEFINED" : methodName.ToUpper())} > | {datetime} - {message}";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Black;

            Console.Out.WriteLine(logEntry);

            Console.ResetColor();

            if (is_io)
            {
                Profiler.EnsureLogs();
                string? path = Environment.GetEnvironmentVariable("LOGS_PATH");
                Profiler.UpdateLogs(logEntry, path);
            }
        }

        public static void Error(Exception exception, string code_pos = "DEFAULT")
        {
            string datetime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

            string logEntry = $"[ERROR/{code_pos.ToUpper()}] < TRACE: {exception.StackTrace} & TARGET: {(exception.TargetSite == null ? "UNDEFINED" : exception.TargetSite.Name)}> " +
                $"{datetime} - {exception.Message}";

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;

            Console.Out.WriteLine(logEntry);

            Console.ResetColor();

            Profiler.EnsureLogs();
            string? path = Environment.GetEnvironmentVariable("LOGS_PATH");
            Profiler.UpdateLogs(logEntry, path);
        }

        public static void Error(string message, string code_pos = "DEFAULT", bool is_io = false)
        {
            var stackframe = new StackFrame(1);

            string? methodName = stackframe.GetMethod()?.Name;

            string datetime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

            string logEntry = $"[ERROR/{code_pos.ToUpper()}] < STACKFRAME: {(methodName == null ? "UNDEFINED" : methodName.ToUpper())} > | {datetime} - {message}";

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;

            Console.Out.WriteLine(logEntry);

            Console.ResetColor();

            if (is_io)
            {
                Profiler.EnsureLogs();
                string? path = Environment.GetEnvironmentVariable("LOGS_PATH");
                Profiler.UpdateLogs(logEntry, path);
            }
        }

        public static void Success(string message, string code_pos = "DEFAULT", bool is_io = false)
        {
            var stackframe = new StackFrame(1);

            string? methodName = stackframe.GetMethod()?.Name;

            string datetime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

            string logEntry = $"[SUCCESS/{code_pos.ToUpper()}] < STACKFRAME: {(methodName == null ? "UNDEFINED" : methodName.ToUpper())} > | {datetime} - {message}";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;

            Console.Out.WriteLine(logEntry);

            Console.ResetColor();

            if (is_io)
            {
                Profiler.EnsureLogs();
                string? path = Environment.GetEnvironmentVariable("LOGS_PATH");
                Profiler.UpdateLogs(logEntry, path);
            }
        }

        public static class Customs
        {
            public static void Log(string message, string type = "CUSTOM", string code_pos = "DEFAULT",
                ColorsPair? colors = null,
                bool is_io = false, string? folder = null, string? file = null)
            {
                var stackframe = new StackFrame(1);

                string? methodName = stackframe.GetMethod()?.Name;

                string logEntry = $"[{type.ToUpper()}/{code_pos.ToUpper()}] < STACKFRAME: {(methodName == null ? "UNDEFINED" : methodName.ToUpper())} > | {DateTime.Now:dd.MM.yyyy HH:mm:ss} - {message}";

                if (colors == null)
                    colors = new ColorsPair();

                Console.BackgroundColor = colors.BackgroundColor;
                Console.ForegroundColor = colors.ForegroundColor;

                Console.Out.WriteLine(logEntry);

                Console.ResetColor();

                if (is_io)
                {
                    if (folder == null || folder == "" || folder.Length < 1)
                        folder = "logs";

                    if (file == null || file == "" || file.Length < 1)
                        file = $"session-{DateTime.Now:dd-MM-yyyy}.logs";

                    DirectoryInfo dir = new(folder);

                    if (dir.Exists)
                        dir.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                    else
                    {
                        dir.Create();
                        dir.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                    }

                    if (!File.Exists(Path.Combine(folder, file)))
                        using (FileStream stream = File.Create(Path.Combine(folder, file)))
                        {
                            byte[] msg = Encoding.UTF8.GetBytes(logEntry);

                            stream.Write(msg, 0, msg.Length);
                            stream.Close();
                        }
                }
            }
        }
    }
}
