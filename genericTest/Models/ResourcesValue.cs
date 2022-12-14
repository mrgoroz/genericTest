namespace genericTest.Models
{
    internal class ResourcesValue<T>
    {
        public T value { get; set; }
        public DateTime insertTime { get; set; }
        public ResourcesValue(T value, DateTime insertTime)
        {
            this.value = value;
            this.insertTime = insertTime;
        }
    }
}
