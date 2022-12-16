namespace genericTest.Models
{
    internal class ResourcesValue<T>
    {
        public T m_value { get; set; }
        public DateTime InsertTime { get; set; }
        public ResourcesValue(T value, DateTime insertTime)
        {
            this.m_value = value;
            this.InsertTime = insertTime;
        }
    }
}
