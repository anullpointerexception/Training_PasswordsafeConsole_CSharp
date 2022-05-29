namespace PasswordSafeConsole
{
    internal interface IMasterPasswordRepository
    {
       bool MasterPasswordIsEqualTo(string masterPwToCompare);

       void SetMasterPasswordPlain(string masterPw);
    }
}