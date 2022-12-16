namespace genericTest.Models
{
    internal interface IResourceReader<T>
    {
        public Task<T> GetValue();
    }
}
