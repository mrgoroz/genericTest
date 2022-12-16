namespace genericTest.IServices
{
    internal interface IResourceReader<T>
    {
        public Task<T> GetValue();
    }
}
