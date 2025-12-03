namespace Shuttle.Core.Compression;

public class NullCompressionAlgorithm : ICompressionAlgorithm
{
    public string Name => "null";

    public async Task<byte[]> CompressAsync(byte[] bytes, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(bytes);
    }

    public async Task<byte[]> DecompressAsync(byte[] bytes, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(bytes);
    }
}