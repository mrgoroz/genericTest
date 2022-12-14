using genericTest.Models;
using Newtonsoft.Json;

namespace genericTest.Services
{
    internal class FileSystemReader<T> : IFileSystemReader<T>
    {

        string mode { get; set; }
        int ttl { get; set; }
        string path { get; set; }

        private readonly IWebServiceReader<T> _next;
        private readonly IDateTimeChecker _dateTimeChecker;

        public FileSystemReader(IWebServiceReader<T> next, IDateTimeChecker dateTimeChecker)
        {
            _next = next;
            _dateTimeChecker = dateTimeChecker;
            mode = "RW";
            ttl = 4;
            path = "Value.json";
        }

        public async Task<T> getValue()
        {
            ResourcesValue<T> resourceValue = null;
            if (File.Exists(path))
            {
                using (StreamReader r = new StreamReader(path))
                {
                    string json = await r.ReadToEndAsync();
                    resourceValue = JsonConvert.DeserializeObject<ResourcesValue<T>>(json);
                }
            }
            if (resourceValue != null && _dateTimeChecker.check(resourceValue.insertTime, ttl))
            {
                return resourceValue.value;
            }
            T newValue = await _next.getValue();
            if (mode.Contains('W'))
            {
                resourceValue = new ResourcesValue<T>(newValue, DateTime.Now);
                await File.WriteAllTextAsync(path, JsonConvert.SerializeObject(resourceValue));
            }
            return newValue;
        }
    }
}
