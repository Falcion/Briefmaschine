using System.Text;

namespace Briefmaschine.Coroutines
{
#pragma warning disable S101
    /// <summary>
    /// Internal class containing IO methods for logger's static methods
    /// </summary>
    internal static class LIO
#pragma warning restore S101
    {
        /// <summary>
        /// Const string representing default common message for exceptions in IO methods
        /// </summary>
        private const string PATH_EXCEPTION_MESSAGE = "Parsing process got nullable result via trying to serialize path's variable of update method exception was made.";

        /// <summary>
        /// Method working with basic IO and preparing it's instance for logger
        /// </summary>
        /// <param name="dest">
        /// String representing folder's name for IO instance of logger
        /// </param>
        ///<param name="file">
        /// String representing filename for IO instance of logger
        /// </param>
        /// <param name="is_process">
        /// Boolean parameter representing will given path be written into environment variable
        /// </param>
        internal static void EnsureIO(string dest = ".audit", string file = "", bool is_process = true)
        {
            if (dest == string.Empty)
                dest = ".audit";

            if (file == string.Empty)
                file = $"session-{DateTime.Now:dd-MM-yyyy}.logs";

            string path = Path.Combine(dest, file);

            if(!Directory.Exists(dest))
                Directory.CreateDirectory(dest);

            if(!File.Exists(path))
                using(var stream = File.Create(path))
                {
                    byte[] message = Encoding.UTF8.GetBytes($"[INFO/INIT][STACKFRAME: UNDEFINED] | {DateTime.Now:dd.MM.yyyy HH:mm:ss} - Created first instance for logs!" + "\n");

                    stream.Write(message);
                    stream.Close();
                }

            if (is_process)
                Environment.SetEnvironmentVariable("LOGS_PATH", path);
        }

        /// <summary>
        /// Method updating and appending message into existing IO instance of logger
        /// </summary>
        /// <param name="message">
        /// String representing message which will be appended into IO instance
        /// </param>
        /// <param name="path">
        /// String representing fullpath to future IO file of logs which in case of nullable, method replaces with ENV value
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Exception representing a situation, when method can't create any form of path to IO instance from given parameter
        /// </exception>
        internal static void InjectIO(string message, string? path)
        {
            if(path != null)
            {
                string? dest = Path.GetDirectoryName(path);

#pragma warning disable S3928
                if (dest == null || dest == string.Empty)
                    throw new ArgumentNullException(nameof(dest), PATH_EXCEPTION_MESSAGE);

                string? file = Path.GetFileName(path);

                if (file == null || file == string.Empty)
                    throw new ArgumentNullException(nameof(file), PATH_EXCEPTION_MESSAGE);
#pragma warning restore S3928

                EnsureIO(dest, file);
            }
            else
            {
                string? keypath = Environment.GetEnvironmentVariable("LOGS_PATH");

                if(keypath == null) EnsureIO();
            }

            path = Environment.GetEnvironmentVariable("LOGS_PATH");

            using(var writer = File.AppendText(path!))
            {
                writer.Write(message + "\n");
                writer.Close();
            }
        }
    }
}
