using Serilog;

namespace PasswordSafeConsole
{
    public class Info : ILoggerFactory
    {
        public Type GetErrorType()
        {
            return Type.INFO;
        }

        public void LogInformation(string message)
        {
            Log.Error(message);
        }
    }
}