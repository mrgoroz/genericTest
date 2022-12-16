namespace genericTest.Models
{
    internal interface IDateTimeChecker
    {
        public bool Check(DateTime time, int ttl);
    }
}
