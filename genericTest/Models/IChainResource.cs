namespace genericTest.Models
{
    internal interface IChainResource<T>
    {
        public Task<T> GetValue();
    }
}
