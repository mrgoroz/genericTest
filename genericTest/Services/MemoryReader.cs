using genericTest.Models;

namespace genericTest.Services
{
    internal class MemoryReader<T> : IMemoryReader<T>
    {
        string mode { get; set; }
        int ttl { get; set; }
        private ResourcesValue<T> resourceValue;

        private readonly IFileSystemReader<T> _next;
        private readonly IDateTimeChecker _dateTimeChecker;


        public MemoryReader(IFileSystemReader<T> next, IDateTimeChecker dateTimeChecker)
        {
            _next = next;
            _dateTimeChecker = dateTimeChecker;
            mode = "RW";
            ttl = 1;
        }

        async Task<T> IResourceReader<T>.getValue()
        {
            if (resourceValue != null && _dateTimeChecker.check(resourceValue.insertTime, ttl))
            {
                return resourceValue.value;
            }
            T newValue = await _next.getValue();
            if (mode.Contains('W'))
            {
                resourceValue = new ResourcesValue<T>(newValue, DateTime.Now);
            }
            return newValue;
        }

    }
}
