using Serilog;

namespace PasswordSafeConsole
{
    public class LoggerFactory
    {
        public static ILoggerFactory GetLoggerType(Type type)
        {
            ILoggerFactory logger = null;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            logger = type switch
            {
                Type.DEBUG => new DEBUG(),
                Type.INFO => new Info(),
                Type.ERROR => new Error(),
                _ => logger
            };

            return logger;
        }
    }
}