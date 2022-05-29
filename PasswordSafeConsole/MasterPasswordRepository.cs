using System.IO;

namespace PasswordSafeConsole
{
    internal class MasterPasswordRepository : IMasterPasswordRepository
    {
        private string masterPasswordPath;

        public MasterPasswordRepository(string masterPasswordPath)
        {
            this.masterPasswordPath = masterPasswordPath;
        }

        public bool MasterPasswordIsEqualTo(string masterPwToCompare)
        {
            return File.Exists(this.masterPasswordPath) &&
                masterPwToCompare == File.ReadAllText(this.masterPasswordPath);
        }

        public void SetMasterPasswordPlain(string masterPw)
        {
            File.WriteAllText(this.masterPasswordPath, masterPw);
        }
    }
}