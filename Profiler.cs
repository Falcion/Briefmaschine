using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briefsmaschine
{
    public static class Profiler
    {
        public static string EnsureLogs(string folder = "logs")
        {
            string filename = $"session-{DateTime.Now:dd-MM-yyyy}.logs";

            DirectoryInfo dir = new(folder);

            if (dir.Exists)
                dir.Attributes = FileAttributes.Directory | FileAttributes.Hidden | FileAttributes.ReadOnly;
            else
            {
                dir.Create();
                dir.Attributes = FileAttributes.Directory | FileAttributes.Hidden | FileAttributes.ReadOnly;
            }

            Environment.SetEnvironmentVariable("LOGS_PATH", Path.Combine(folder, filename));

            if (!File.Exists(Path.Combine(folder, filename)))
                using (FileStream stream = File.Create(Path.Combine(folder, filename)))
                {
                    byte[] msg = Encoding.UTF8.GetBytes($"[SUCCESS/CORE] < STACKFRAME: NULL > | {DateTime.Now:dd.MM.yyyy HH:mm:ss} - Created first instance for logs!\n");

                    stream.Write(msg, 0, msg.Length);
                    stream.Close();
                }
            else
                return "Environment variable for logs path had already existed! None changes had been made.";

            return "Created environment variable for logs path successfully!";
        }

        public static string UpdateLogs(string message, string? path)
        {
            using (StreamWriter stream = File.AppendText((path ?? "logs/default.logs")))
            {
                stream.WriteLine(message);
                stream.Close();
            }

            return "Updated logs session successfully!";
        }
    }
}
