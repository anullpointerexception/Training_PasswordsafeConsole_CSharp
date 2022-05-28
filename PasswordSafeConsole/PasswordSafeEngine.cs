using System.Collections.Generic;

namespace PasswordSafeConsole
{
    internal class PasswordSafeEngine
    {
        private CipherFacility cipherFacility;
        private IDataSourceLayer multiFileDataSourceLayer;

        public PasswordSafeEngine(CipherFacility cipherFacility, IDataSourceLayer dataSourceLayer)
        {
            this.cipherFacility = cipherFacility;
            this.multiFileDataSourceLayer = dataSourceLayer;
        }

        internal IEnumerable<string> GetStoredPasswords()
        {
            return this.multiFileDataSourceLayer.GetAllNamesOfPasswords();
        }

        internal string GetPassword(string passwordName)
        {
            byte[] password = this.multiFileDataSourceLayer.GetPasswordCipher(passwordName);
            return this.cipherFacility.Decrypt(password);
        }

        internal void AddNewPassword(PasswordInfo passwordInfo)
        {
            byte[] cipher = this.cipherFacility.Encrypt(passwordInfo.Password);
            this.multiFileDataSourceLayer.StorePassword(passwordInfo, cipher);
        }

        internal void DeletePassword(string passwordName)
        {
            this.multiFileDataSourceLayer.DeletePasswordData(passwordName);
        }
    }
}