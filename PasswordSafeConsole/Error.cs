using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace PasswordSafeConsole
{
    class Error : ILoggerFactory
    {
        public Type GetErrorType()
        {
            return Type.ERROR;
        }

        public void LogInformation(string message)
        {
            Log.Error(message);
        }
    }
}
