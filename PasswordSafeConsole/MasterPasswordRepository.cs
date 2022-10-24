using System.IO;
using System.Net;

namespace PasswordSafeConsole
{
    internal class MasterPasswordRepository
    {
        private string masterPasswordPath;
        private static MasterPasswordRepository instance;
        

        private MasterPasswordRepository(string masterPasswordPath)
        {
            this.masterPasswordPath = masterPasswordPath;
        }

        internal static MasterPasswordRepository Instance()
        {
            if (instance == null)
            {
                        instance = new MasterPasswordRepository("./master.pw");
                              
            }
            return instance;
        }

        internal bool MasterPasswordIsEqualTo(string masterPwToCompare)
        {
            return File.Exists(this.masterPasswordPath) && 
                masterPwToCompare == File.ReadAllText(this.masterPasswordPath);
        }

        internal void SetMasterPasswordPlain(string masterPw)
        {
            File.WriteAllText(this.masterPasswordPath, masterPw);
        }
    }
}