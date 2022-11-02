using System;
using System.IO;
using System.Linq;
using static System.Net.WebRequestMethods;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace PasswordSafeConsole
{
    public class Program
    {
        private static MasterPasswordRepository masterRepository = MasterPasswordRepository.Instance();
        private static PasswordSafeEngine passwordSafeEngine = null;

        public static void Main(String[] args)
        {
            Console.WriteLine("Welcome to Passwordsafe");

            ILoggerFactory iLoggerFactory = null;

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
                                try
                                {
                                    passwordSafeEngine = new PasswordSafeEngine("./passwords.pw", new CipherFacility(masterPw));
                                    // Console.WriteLine("unlocked");
                                    iLoggerFactory = LoggerFactory.GetLoggerType(Type.Info);
                                    iLoggerFactory.LogInformation("unlocked");

                                }
                                catch (Exception ex)
                                {
                                    iLoggerFactory = LoggerFactory.GetLoggerType(Type.Error);
                                    iLoggerFactory.LogInformation(ex.ToString());
                                }
                            }
                            else
                            {
                                // Console.WriteLine("master password did not match ! Failed to unlock.");
                                iLoggerFactory = LoggerFactory.GetLoggerType(Type.Error);
                                iLoggerFactory.LogInformation("master password did not match! Failed to unlock.");
                            }
                            break;
                        }
                    case 2:
                        {
                            if (unlocked)
                            {
                                try
                                {
                                    passwordSafeEngine.GetStoredPasswords().ToList().ForEach(pw => Console.WriteLine(pw));
                                }
                                catch (Exception ex)
                                {
                                    iLoggerFactory = LoggerFactory.GetLoggerType(Type.Error);
                                    iLoggerFactory.LogInformation(ex.ToString());
                                }
                            }
                            else
                            {
                                iLoggerFactory = LoggerFactory.GetLoggerType(Type.Error);
                                iLoggerFactory.LogInformation("Please unlock first by entering the master password."); 
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
                                iLoggerFactory = LoggerFactory.GetLoggerType(Type.Error);
                                iLoggerFactory.LogInformation("Please unlock first by entering the master password.");
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
                                try
                                {
                                    passwordSafeEngine.AddNewPassword(new PasswordInfo(password, passwordName));
                                    iLoggerFactory = LoggerFactory.GetLoggerType(Type.Info);
                                    iLoggerFactory.LogInformation("New password added");
                                    
                                }
                                catch (Exception ex)
                                {
                                    iLoggerFactory = LoggerFactory.GetLoggerType(Type.Error);
                                    iLoggerFactory.LogInformation(ex.ToString());
                                }
                            }
                            else
                            {
                                iLoggerFactory = LoggerFactory.GetLoggerType(Type.Error);
                                iLoggerFactory.LogInformation("Please unlock first by entering the master password.");
                            }
                            break;
                        }
                    case 5:
                        {
                            if (unlocked)
                            {
                                Console.WriteLine("Enter password name");
                                String passwordName = Console.ReadLine();
                                try
                                {
                                    passwordSafeEngine.DeletePassword(passwordName);
                                    iLoggerFactory = LoggerFactory.GetLoggerType(Type.Info);
                                    iLoggerFactory.LogInformation("Password removed!");

                                }
                                catch (Exception ex)
                                {
                                    iLoggerFactory = LoggerFactory.GetLoggerType(Type.Error);
                                    iLoggerFactory.LogInformation(ex.ToString());

                                }
                            }
                            else
                            {
                                Logger.WriteLog("Please unlock first by entering the master password.", Logger.Type.ERROR);
                            }
                            break;
                        }
                    case 6:
                        {
                            unlocked = false;
                            passwordSafeEngine = null;
                            string masterPw;
                            string confirmPw;
                            do // Added confirmation
                            {
                                Console.WriteLine("Enter new master password ! (Warning you will loose all already stored passwords)");
                                masterPw = Console.ReadLine();
                                Console.WriteLine("Please confirm master password ! ");
                                confirmPw = Console.ReadLine();
                                if (masterPw != confirmPw)
                                {
                                    // Console.WriteLine("Passwords do not match! Try again! ");
                                    iLoggerFactory = LoggerFactory.GetLoggerType(Type.Error);
                                    iLoggerFactory.LogInformation("Passwords do not match! Try again! ");
                                }
                                else
                                {
                                    iLoggerFactory = LoggerFactory.GetLoggerType(Type.Info);
                                    iLoggerFactory.LogInformation("Passwords updated");
                                }
                            } while (masterPw != confirmPw);

                            masterRepository.SetMasterPasswordPlain(masterPw);
                            // urgent hotfix delete old passwords after changing the master
                            if (Directory.Exists("./passwords.pw"))
                            {
                                try
                                {
                                    Directory.Delete("./passwords.pw", true);
                                    iLoggerFactory = LoggerFactory.GetLoggerType(Type.Info);
                                    iLoggerFactory.LogInformation("Successfully deleted!");

                                }
                                catch (Exception ex)
                                {
                                    iLoggerFactory = LoggerFactory.GetLoggerType(Type.Error);
                                    iLoggerFactory.LogInformation(ex.ToString());
                                }

                            }
                            break;

                        }
                    default:
                        {
                            iLoggerFactory = LoggerFactory.GetLoggerType(Type.Error);
                            iLoggerFactory.LogInformation("Invalid input");
                            break;
                        }

                }
            }

            Console.WriteLine("Good bye !");
        }
    }
}
