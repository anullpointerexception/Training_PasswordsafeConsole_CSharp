using System;
using System.Text.RegularExpressions;

namespace PasswordSafeConsole.Plugins
{
    internal class TestPasswordStrengthPlugin : PluginBase
    {
        public override void Execute()
        {
            Console.WriteLine("Enter passwprd to test: ");
            string passwordToTest = Console.ReadLine();
            Regex containsSpecialCharacters = new Regex(".*[&,?,!,*,+,-].*");
            if (passwordToTest.Length > 8 && containsSpecialCharacters.IsMatch(passwordToTest))
            {
                Console.WriteLine("Password is strong.");
            } else
            {
                Console.WriteLine("Password is weak.");
            }
        }
    }
}