using System;
using System.IO;
using System.Threading;

namespace Common
{
    public class Logger
    {
        public enum LogLevel
        {
            None,
            Trace,
            Debug
        }

        public LogLevel Level { get; set; } = LogLevel.None;
        public string Filepath { get; set; }
        public bool Append { get; set; }

        public Logger(string filepath, LogLevel level, bool append = true) : this(filepath)
        {
            Level = level;
            Append = append;

            if (File.Exists(filepath) && !Append)
            {
                File.Delete(filepath);
            }
        }

        public Logger(string filepath)
        {
            Filepath = filepath;

            var path = Path.GetDirectoryName(filepath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                if (!Directory.Exists(path))
                    throw new Exception($"Unable to create path: {path}");
            }
        }

        private static ReaderWriterLockSlim locker = new ReaderWriterLockSlim();
        public void Write(LogLevel level, string info)
        {
            //var dir = Environment.CurrentDirectory;
            if (Level != LogLevel.None && level <= Level)
            {
                locker.EnterWriteLock();
                try
                {
                    using (var writer = new StreamWriter(Filepath, true))
                    {
                        var line = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} {level}: {info}";
                        writer.WriteLine(line);
                    }
                }
                finally
                {
                    locker.ExitWriteLock();
                }
            }
        }
    }
}
