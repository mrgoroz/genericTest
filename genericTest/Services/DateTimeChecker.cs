using genericTest.Models;

namespace genericTest.Services
{
    internal class DateTimeChecker : IDateTimeChecker
    {

        public bool Check(DateTime time, int ttl)
        {
            return DateTime.Now < time.AddHours(ttl);
        }
    }
}
