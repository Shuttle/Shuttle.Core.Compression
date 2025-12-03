using System.IO.Compression;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression;

public class DeflateCompressionAlgorithm : ICompressionAlgorithm
{
    public string Name => "Deflate";

    public async Task<byte[]> CompressAsync(byte[] bytes, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNull(bytes);

        await using var compressed = MemoryStreamCache.Manager.GetStream();
        await using var deflate = new DeflateStream(compressed, CompressionMode.Compress, true);

        await deflate.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);

        await deflate.FlushAsync(cancellationToken);

        return compressed.ToArray();
    }

    public async Task<byte[]> DecompressAsync(byte[] bytes, CancellationToken cancellationToken = default)
    {
        Guard.AgainstNull(bytes);

        await using var decompressed = MemoryStreamCache.Manager.GetStream();

        var deflate = new DeflateStream(new MemoryStream(bytes), CompressionMode.Decompress);

        await using (deflate.ConfigureAwait(false))
        {
            await deflate.CopyToAsync(decompressed, cancellationToken).ConfigureAwait(false);
        }

        return decompressed.ToArray();
    }
}