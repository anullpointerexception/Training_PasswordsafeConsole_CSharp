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
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            logger = type switch
            {
                Type.Debug => new Debug(), 
                Type.Info => new Info(),
                Type.Error => new Error(),
                _ => logger
            };

            return logger;
        }
    }
}