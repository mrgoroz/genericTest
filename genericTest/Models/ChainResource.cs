using genericTest.Services;

namespace genericTest.Models
{
    internal class ChainResource<T> : IChainResource<T>
    {
        public IMemoryReader<T> _memoryReader { get; set; }

        public ChainResource(IMemoryReader<T> memoryReader)
        {
            _memoryReader = memoryReader;
        }

        async Task<T> IChainResource<T>.GetValue()
        {
            return await _memoryReader.getValue();
        }
    }
}
