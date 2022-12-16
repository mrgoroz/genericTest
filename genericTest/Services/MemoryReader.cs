using genericTest.IServices;
using genericTest.Models;

namespace genericTest.Services
{
    internal class MemoryReader<T> : IMemoryReader<T>
    {
        string Mode { get; set; }
        int Ttl { get; set; }
        private ResourcesValue<T> resourceValue;

        private readonly IFileSystemReader<T> _next;
        private readonly IDateTimeChecker _dateTimeChecker;


        public MemoryReader(IFileSystemReader<T> next, IDateTimeChecker dateTimeChecker)
        {
            _next = next;
            _dateTimeChecker = dateTimeChecker;
            Mode = "RW";
            Ttl = 1;
        }

        async Task<T> IResourceReader<T>.GetValue()
        {
            if (resourceValue != null && _dateTimeChecker.Check(resourceValue.InsertTime, Ttl))
            {
                return resourceValue.m_value;
            }
            T newValue = await _next.GetValue();
            if (Mode.Contains('W'))
            {
                resourceValue = new ResourcesValue<T>(newValue, DateTime.Now);
            }
            return newValue;
        }

    }
}
