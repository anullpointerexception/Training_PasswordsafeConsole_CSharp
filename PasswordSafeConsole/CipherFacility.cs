using System.Security.Cryptography;
using System.Text;

namespace PasswordSafeConsole
{
    internal class CipherFacility
    {
        private string masterPw;

        public CipherFacility(string masterPw)
        {
            this.masterPw = masterPw;
        }

        public string Decrypt(byte[] crypted)
        {
            var key = GetKey(this.masterPw);

            using (var aes = Aes.Create())
            using (var decryptor = aes.CreateDecryptor(key, key))
            {
                var decryptedBytes = decryptor
                    .TransformFinalBlock(crypted, 0, crypted.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }

        public byte[] Encrypt(string plain)
        {
            var key = GetKey(this.masterPw);
            using (var aes = Aes.Create())
            using (var encryptor = aes.CreateEncryptor(key, key))
            {
                var plainText = Encoding.UTF8.GetBytes(plain);
                return encryptor.TransformFinalBlock(plainText, 0, plainText.Length);
            }
        }

        private static byte[] GetKey(string password)
        {
            var keyBytes = Encoding.UTF8.GetBytes(password);
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(keyBytes);
            }
        }
    }
}