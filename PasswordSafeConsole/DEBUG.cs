using Serilog;

namespace PasswordSafeConsole
{
    public class Debug : ILoggerFactory
    {
        public Type GetErrorType()
        {
            return Type.Debug;
        }

        public void LogInformation(string message)
        {
            Log.Debug(message);

        }
    }
}