using System;
using System.IO;
using System.Linq;

namespace PasswordSafeConsole
{
    public class Program
    {
        private static MasterPasswordRepository masterRepository = MasterPasswordRepository.Instance();
        private static PasswordSafeEngine passwordSafeEngine = null;

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
                                    Logger.WriteLog("unlocked", Logger.Type.INFO);

                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteLog(ex.ToString(), Logger.Type.ERROR);
                                }
                            }
                            else
                            {
                                // Console.WriteLine("master password did not match ! Failed to unlock.");
                                Logger.WriteLog("master password did not match ! Failed to unlock.", Logger.Type.ERROR);
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
                                    Logger.WriteLog(ex.ToString(), Logger.Type.ERROR);
                                }
                            }
                            else
                            {
                                Logger.WriteLog("Please unlock first by entering the master password.", Logger.Type.ERROR);
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
                                Logger.WriteLog("Please unlock first by entering the master password.", Logger.Type.ERROR);
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
                                    Logger.WriteLog("New Password added", Logger.Type.INFO);
                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteLog(ex.ToString(), Logger.Type.ERROR);
                                }
                            }
                            else
                            {
                                Logger.WriteLog("Please unlock first by entering the master password.", Logger.Type.ERROR);
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
                                    Logger.WriteLog("Password removed", Logger.Type.INFO);

                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteLog(ex.ToString(), Logger.Type.ERROR);

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
                                    Logger.WriteLog("Passwords do not match! Try again! ", Logger.Type.ERROR);
                                }
                            } while (masterPw != confirmPw);

                            masterRepository.SetMasterPasswordPlain(masterPw);
                            // urgent hotfix delete old passwords after changing the master
                            if (Directory.Exists("./passwords.pw"))
                            {
                                try
                                {
                                    Directory.Delete("./passwords.pw", true);
                                    Logger.WriteLog("successfully deleted", Logger.Type.INFO);

                                }
                                catch (Exception ex)
                                {
                                    Logger.WriteLog(ex.ToString(), Logger.Type.ERROR);
                                }

                            }
                            break;
                        }
                    default:
                        {
                            Logger.WriteLog("Invalid input", Logger.Type.ERROR);
                            break;
                        }

                }
            }

            Console.WriteLine("Good bye !");
        }
    }
}
