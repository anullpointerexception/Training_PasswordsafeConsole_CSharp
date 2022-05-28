using System;
using System.Collections.Generic;

namespace PasswordSafeConsole.Subscriber
{
    public class UserTrackingSubscriber : IUserTrackingSubscriber
    {
        private List<HistoricalUserSelection> historicalUserSelections = new List<HistoricalUserSelection>();

        public record HistoricalUserSelection(int Selection, DateTime TimeStamp);

        public void ForwardSelection(int selection)
        {
            historicalUserSelections.Add(new HistoricalUserSelection(selection, DateTime.Now));

            Console.WriteLine("User Tracking data state: ");
            this.historicalUserSelections.ForEach(item => Console.WriteLine(item));
        }
    }
}
