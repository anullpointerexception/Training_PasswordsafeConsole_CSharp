using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordSafeConsole
{
    public enum Type // error types, there can be added new types later
    {
        Error,
        Info,
        Debug
    }
    public interface ILoggerFactory
    {
        Type GetErrorType();

        void LogInformation(string message);
    }
}
