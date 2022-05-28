namespace PasswordSafeConsole
{
    internal static class PasswordSafeEngingeFactoryMethod
    {
        internal static PasswordSafeEngine CreatePasswordSafeEngine(string masterPw, string passwordFilePath)
        {
            return new PasswordSafeEngine(passwordFilePath, new CipherFacility(masterPw));
        }
    }
}