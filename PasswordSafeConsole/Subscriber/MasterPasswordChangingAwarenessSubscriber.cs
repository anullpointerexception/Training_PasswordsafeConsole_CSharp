using PasswordSafeConsole.Subscriber;
using System;

namespace PasswordSafeConsole
{
    internal class MasterPasswordChangingAwarenessSubscriber : IUserTrackingSubscriber
    {
        public void ForwardSelection(int selection)
        {
            if (selection == 6)
            {
                Console.WriteLine("Please be awara that chaging the master password leads to data loss !");
            }
        }
    }
}