using System.Threading.Tasks;

namespace Shuttle.Core.Compression
{
    public class NullCompressionAlgorithm : ICompressionAlgorithm
    {
        public string Name => "null";

        public Task<byte[]> Compress(byte[] bytes)
        {
            return Task.FromResult(bytes);
        }

        public Task<byte[]> Decompress(byte[] bytes)
        {
            return Task.FromResult(bytes);
        }
    }
}