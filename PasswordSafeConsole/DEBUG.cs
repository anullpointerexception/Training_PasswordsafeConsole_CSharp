using Serilog;

namespace PasswordSafeConsole
{
    public class DEBUG : ILoggerFactory
    {
        public Type GetErrorType()
        {
            return Type.DEBUG;
        }

        public void LogInformation(string message)
        {
            Log.Debug(message);
        }
    }
}