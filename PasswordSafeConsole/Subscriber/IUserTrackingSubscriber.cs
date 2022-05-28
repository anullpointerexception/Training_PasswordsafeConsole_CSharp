namespace PasswordSafeConsole.Subscriber
{
    public interface IUserTrackingSubscriber
    {
        void ForwardSelection(int selection);
    }
}