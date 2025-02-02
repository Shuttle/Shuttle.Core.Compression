using System.Threading.Tasks;

namespace Shuttle.Core.Compression;

public class NullCompressionAlgorithm : ICompressionAlgorithm
{
    public string Name => "null";

    public async Task<byte[]> CompressAsync(byte[] bytes)
    {
        return await Task.FromResult(bytes);
    }

    public async Task<byte[]> DecompressAsync(byte[] bytes)
    {
        return await Task.FromResult(bytes);
    }
}