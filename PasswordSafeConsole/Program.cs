using PasswordSafeConsole.Subscriber;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PasswordSafeConsole
{
    public class Program
    {
        private static MasterPasswordRepository masterRepository = new MasterPasswordRepository("./master.pw");
        private static PasswordSafeEngine passwordSafeEngine = null;
        private static List<IUserTrackingSubscriber> userTrackingSubscribers = new List<IUserTrackingSubscriber>() { new UserTrackingSubscriber(), new MasterPasswordChangingAwarenessSubscriber() };

        public static void Main(String[] args)
        {
            Console.WriteLine("Welcome to Passwordsafe");

            bool abort = false;
            bool unlocked = false;
            while (!abort)
            {
                Console.WriteLine("Enter master (1), show all (2), show single (3), add (4), delete(5), set new master (6), Abort (0)");
                int input = 0;
                if (!int.TryParse(Console.ReadLine(), out input))
                {
                    input = -1;
                }
                ForwardUserSelection(input);
                switch (input)
                {
                    case 0:
                        {
                            abort = true;
                            break;
                        }
                    case 1:
                        {
                            Console.WriteLine("Enter master password");
                            String masterPw = Console.ReadLine();
                            unlocked = masterRepository.MasterPasswordIsEqualTo(masterPw);
                            if (unlocked)
                            {
                                passwordSafeEngine = new PasswordSafeEngine("./passwords.pw", new CipherFacility(masterPw));
                                Console.WriteLine("unlocked");
                            }
                            else
                            {
                                Console.WriteLine("master password did not match ! Failed to unlock.");
                            }
                            break;
                        }
                    case 2:
                        {
                            if (unlocked)
                            {
                                passwordSafeEngine.GetStoredPasswords().ToList().ForEach(pw => Console.WriteLine(pw));
                            }
                            else
                            {
                                Console.WriteLine("Please unlock first by entering the master password.");
                            }
                            break;
                        }
                    case 3:
                        {
                            if (unlocked)
                            {
                                Console.WriteLine("Enter password name");
                                String passwordName = Console.ReadLine();
                                Console.WriteLine(passwordSafeEngine.GetPassword(passwordName));
                            }
                            else
                            {
                                Console.WriteLine("Please unlock first by entering the master password.");
                            }
                            break;
                        }
                    case 4:
                        {
                            if (unlocked)
                            {
                                Console.WriteLine("Enter new name of password");
                                String passwordName = Console.ReadLine();
                                Console.WriteLine("Enter password");
                                String password = Console.ReadLine();
                                passwordSafeEngine.AddNewPassword(new PasswordInfo(password, passwordName));
                            }
                            else
                            {
                                Console.WriteLine("Please unlock first by entering the master password.");
                            }
                            break;
                        }
                    case 5:
                        {
                            if (unlocked)
                            {
                                Console.WriteLine("Enter password name");
                                String passwordName = Console.ReadLine();
                                passwordSafeEngine.DeletePassword(passwordName);
                            }
                            else
                            {
                                Console.WriteLine("Please unlock first by entering the master password.");
                            }
                            break;
                        }
                    case 6:
                        {
                            unlocked = false;
                            passwordSafeEngine = null;
                            Console.WriteLine("Enter new master password ! (Warning you will loose all already stored passwords)");
                            String masterPw = Console.ReadLine();
                            masterRepository.SetMasterPasswordPlain(masterPw);
                            // urgent hotfix delete old passwords after changing the master
                            if (Directory.Exists("./passwords.pw"))
                            {
                                Directory.Delete("./passwords.pw", true);
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid input");
                            break;
                        }

                }
            }

            Console.WriteLine("Good bye !");
        }

        private static void ForwardUserSelection(int input)
        {
            userTrackingSubscribers.ForEach(subsrciber => subsrciber.ForwardSelection(input));
        }
    }
}
