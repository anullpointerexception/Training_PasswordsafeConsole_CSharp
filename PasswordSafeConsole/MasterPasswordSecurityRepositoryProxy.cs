using System;

namespace PasswordSafeConsole
{
    internal class MasterPasswordSecurityRepositoryProxy : IMasterPasswordRepository
    {
        private IMasterPasswordRepository masterPasswordRepository;
        private int numberOfEnteredWrongPasswords;

        public MasterPasswordSecurityRepositoryProxy(IMasterPasswordRepository masterPasswordRepository)
        {
            this.masterPasswordRepository = masterPasswordRepository;
        }

        public bool MasterPasswordIsEqualTo(string masterPwToCompare)
        {
            if (this.numberOfEnteredWrongPasswords > 2)
            {
                Console.Write("Wrong password was entered too often. You are locked now.");
                return false;
            }
            bool passwordMatch = this.masterPasswordRepository.MasterPasswordIsEqualTo(masterPwToCompare);
            if (!passwordMatch)
            {
                this.numberOfEnteredWrongPasswords++;
            }
            else
            {
                this.numberOfEnteredWrongPasswords = 0;
            }
            return passwordMatch;
        }

        public void SetMasterPasswordPlain(string masterPw)
        {
            this.masterPasswordRepository.SetMasterPasswordPlain(masterPw);
        }
    }
}