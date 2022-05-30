using System;

namespace PasswordSafeConsole.Plugins
{
    internal abstract class PluginBase
    {
        public abstract void Execute();

        protected void DisplayText(char[] password)
        {
            Console.WriteLine(password);
        }
    }
}