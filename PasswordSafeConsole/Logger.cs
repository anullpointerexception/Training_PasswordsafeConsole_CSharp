using System;
using System.IO;
namespace PasswordSafeConsole
{

    public class Logger
    {
        public enum Type
        {
            INFO,
            ERROR,
            DEBUG
        }
        public static void WriteLog(string message, Type type)
        {
            Console.WriteLine($"[{type}] : {message}");

            string logPath = "/logs";

            // Preparation for logging into files
            using (StreamWriter write = new StreamWriter(logPath, true))
            {
                write.WriteLine(message);
            }

        }


    }
}
