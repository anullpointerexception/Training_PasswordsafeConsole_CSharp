using System.Collections.Generic;

namespace PasswordSafeConsole
{
    internal interface IDataSourceLayer
    {
        void DeletePasswordData(string passwordName);
        IEnumerable<string> GetAllNamesOfPasswords();
        byte[] GetPasswordCipher(string passwordName);
        void StorePassword(PasswordInfo passwordInfo, byte[] cipher);
    }
}