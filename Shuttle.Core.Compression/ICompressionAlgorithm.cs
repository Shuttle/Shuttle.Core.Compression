using System.Threading.Tasks;

namespace Shuttle.Core.Compression;

public interface ICompressionAlgorithm
{
    string Name { get; }

    Task<byte[]> CompressAsync(byte[] bytes);
    Task<byte[]> DecompressAsync(byte[] bytes);
}