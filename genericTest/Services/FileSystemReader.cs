using genericTest.IServices;
using genericTest.Models;
using Newtonsoft.Json;

namespace genericTest.Services
{
    internal class FileSystemReader<T> : IFileSystemReader<T>
    {

        string Mode { get; set; }
        int Ttl { get; set; }
        string Path { get; set; }

        private readonly IWebServiceReader<T> _next;
        private readonly IDateTimeChecker _dateTimeChecker;

        public FileSystemReader(IWebServiceReader<T> next, IDateTimeChecker dateTimeChecker)
        {
            _next = next;
            _dateTimeChecker = dateTimeChecker;
            Mode = "RW";
            Ttl = 4;
            Path = "Value.json";
        }

        public async Task<T> GetValue()
        {
            ResourcesValue<T> resourceValue = null;
            if (File.Exists(Path))
            {
                using (StreamReader r = new StreamReader(Path))
                {
                    string json = await r.ReadToEndAsync();
                    resourceValue = JsonConvert.DeserializeObject<ResourcesValue<T>>(json);
                }
            }
            if (resourceValue != null && _dateTimeChecker.Check(resourceValue.InsertTime, Ttl))
            {
                return resourceValue.m_value;
            }
            T newValue = await _next.GetValue();
            if (Mode.Contains('W'))
            {
                resourceValue = new ResourcesValue<T>(newValue, DateTime.Now);
                await File.WriteAllTextAsync(Path, JsonConvert.SerializeObject(resourceValue));
            }
            return newValue;
        }
    }
}
