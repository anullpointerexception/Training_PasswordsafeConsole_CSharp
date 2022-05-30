using System;

namespace PasswordSafeConsole.Plugins
{
    internal class SuggestRandomPasswordPlugin : PluginBase
    {
        public override void Execute()
        {
            char[] characterset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVW123456789(/&§".ToCharArray();
            char[] password = new char[10];
            Random rnd = new Random();
            for (int i = 0; i < password.Length; i++)
            {
                password[i] = characterset[rnd.Next(characterset.Length)];
            }
            DisplayText(password);
        }
    }
}