using System;
using System.Collections.Generic;
using System.IO;
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
            return Type.Error;
        }

        public void LogInformation(string message)
        {
            Log.Error(message);
        }
    }
}
