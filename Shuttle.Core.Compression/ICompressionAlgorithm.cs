using System.Threading.Tasks;

namespace Shuttle.Core.Compression
{
    public interface ICompressionAlgorithm
    {
        string Name { get; }

        Task<byte[]> Compress(byte[] bytes);

        Task<byte[]> Decompress(byte[] bytes);
    }
}