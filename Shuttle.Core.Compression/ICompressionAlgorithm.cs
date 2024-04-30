using System.Threading.Tasks;

namespace Shuttle.Core.Compression
{
    public interface ICompressionAlgorithm
    {
        string Name { get; }

        byte[] Compress(byte[] bytes);
        byte[] Decompress(byte[] bytes);
        Task<byte[]> CompressAsync(byte[] bytes);
        Task<byte[]> DecompressAsync(byte[] bytes);
    }
}