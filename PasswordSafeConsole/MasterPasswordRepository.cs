using System.IO;

namespace PasswordSafeConsole
{
    internal class MasterPasswordRepository
    {
        private string masterPasswordPath;

        public MasterPasswordRepository(string masterPasswordPath)
        {
            this.masterPasswordPath = masterPasswordPath;
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