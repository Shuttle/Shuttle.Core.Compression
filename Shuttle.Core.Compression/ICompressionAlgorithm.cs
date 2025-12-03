namespace Shuttle.Core.Compression;

public interface ICompressionAlgorithm
{
    string Name { get; }

    Task<byte[]> CompressAsync(byte[] bytes, CancellationToken cancellationToken = default);
    Task<byte[]> DecompressAsync(byte[] bytes, CancellationToken cancellationToken = default);
}