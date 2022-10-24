using Serilog;
using System;
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

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            //Console.WriteLine($"[{type}] : {message}");

            switch (type)
            {
                case Type.INFO:
                    Log.Information(message);
                    break;
                case Type.ERROR:
                    Log.Error(message);
                    break;
                case Type.DEBUG:
                    Log.Debug(message);
                    break;
                default:
                    Console.WriteLine("Unknown Log");
                    break;

            }

            // string logPath = "/logs";

            // Preparation for logging into files
            /* using (StreamWriter write = new StreamWriter(logPath, true))
            {
                write.WriteLine(message);
            */
        }

    }


}
