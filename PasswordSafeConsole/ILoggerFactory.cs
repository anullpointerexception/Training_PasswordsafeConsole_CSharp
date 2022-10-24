using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordSafeConsole
{
    public enum Type
    {
        ERROR,
        INFO,
        DEBUG
    }
    public interface ILoggerFactory
    {
        Type GetErrorType();

        void LogInformation(string message);


    }
}
