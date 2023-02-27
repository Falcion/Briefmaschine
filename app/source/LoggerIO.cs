using System.Text;

namespace Briefmaschine
{
    /// <summary>
    /// IO submodule for <see cref="Logger"/> implementation and its methods support
    /// </summary>
    public class LoggerIo
    {
        /// <summary>
        /// Default string message instance for exceptions in IO methods
        /// </summary>
        private const string PATH_EXCEPTION_MESSAGE = "Via parsing and trying to contain path's variable (directory or filename) of update's method exception was made.";

        /// <summary>
        /// Ensuring existence of default/given file and dirpaths and creating them in case of non-existence
        /// </summary>
        /// <param name="dest">
        /// Dirpath to given logs's session file
        /// </param>
        /// <param name="filename">
        /// Filename of log's session file
        /// </param>
        /// <param name="isEnv">
        /// Boolean parameter defining will this path be written in ENV
        /// </param>
        public static void EnsureIo(string? dest = ".audit", string? filename = "", bool isEnv = true)
        {
            if (dest == null || dest == "")
                dest = ".audit";

            if (filename == null || filename == "")
                filename = $"session-{DateTime.Now:dd-MM-yyyy}.logs";

            string path = Path.Combine(dest, filename);

            if (!Directory.Exists(dest))
                Directory.CreateDirectory(dest);

            if (!File.Exists(path))
                using (var stream = File.Create(path))
                {
                    byte[] message = Encoding.UTF8.GetBytes($"[INFO/INIT][STACKFRAME: UNDEFINED] | {DateTime.Now:dd.MM.yyyy HH:mm:ss} - Created first instance for logs!" + "\n");

                    stream.Write(message);
                    stream.Close();
                }

            if (isEnv)
                Environment.SetEnvironmentVariable("LOGS_PATH", path);
        }

        /// <summary>
        /// Updating existing log's session full path by appending given message into it
        /// </summary>
        /// <param name="message">
        /// String message which will be appended into given path
        /// </param>
        /// <param name="path">
        /// String which represents full-defined path to log's session file
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when IO submodule can't resolve given path or generate other
        /// </exception>
        public static void UpdateIo(string message, string? path)
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

                EnsureIo(dest, filename);
            }
            else
            {
                /* Double the value of the ENV variable to escape the nullable value.
                 */

                string? pathEnv = Environment.GetEnvironmentVariable("LOGS_PATH");

                if (pathEnv == null) EnsureIo();
            }

            path = Environment.GetEnvironmentVariable("LOGS_PATH");

            using var writer = File.AppendText(path!);

            writer.Write(message + "\n");
            writer.Close();
        }
    }
}
