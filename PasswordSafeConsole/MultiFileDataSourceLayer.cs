using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PasswordSafeConsole
{
    internal class MultiFileDataSourceLayer : IDataSourceLayer
    {
        private string path;

        public MultiFileDataSourceLayer(string path)
        {
            this.path = path;
        }

        public byte[] GetPasswordCipher(string passwordName)
        {
            return File.ReadAllBytes(Path.Combine(this.path, $"{passwordName}.pw"));
        }

        public IEnumerable<string> GetAllNamesOfPasswords()
        {
            if (!Directory.Exists(this.path))
            {
                return Enumerable.Empty<string>();
            }

            return Directory.GetFiles(this.path).ToList().
                Select(f => Path.GetFileName(f)).
                Where(f => f.EndsWith(".pw")).
                Select(f => f.Split(".")[0]);
        }

        public void DeletePasswordData(string passwordName)
        {
            File.Delete(Path.Combine(this.path, $"{passwordName}.pw"));
        }

        public void StorePassword(PasswordInfo passwordInfo, byte[] cipher)
        {
            if (!Directory.Exists(this.path))
            {
                Directory.CreateDirectory(this.path);
            }
            File.WriteAllBytes(
                Path.Combine(this.path, $"{passwordInfo.PasswordName}.pw"),
                cipher);
        }
    }
}