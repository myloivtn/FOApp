using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOApp.Services
{
    class LoggingService
    {
        //    }
        //}
        //using System;
        //using System.IO;

        //public static class LoggingService
        //{
        private static readonly string logFilePath = Path.Combine(FileSystem.AppDataDirectory, "logfile.txt");

        public static void Log(string message)
        {
            string logMessage = $"[{DateTime.Now}] {message}\n";
            File.AppendAllText(logFilePath, logMessage);
        }

        public static string GetLogContents()
        {
            if (File.Exists(logFilePath))
            {
                return File.ReadAllText(logFilePath);
            }
            return "No logs available.";
        }
    }
}
