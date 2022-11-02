using Serilog;

namespace PasswordSafeConsole
{
    public class Info : ILoggerFactory
    {
        public Type GetErrorType()
        {
            return Type.Info;
        }

        public void LogInformation(string message)
        {
            Log.Information(message);
        }
    }
}