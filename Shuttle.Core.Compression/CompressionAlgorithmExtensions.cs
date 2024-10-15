using System.IO;
using System.Threading.Tasks;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression;

public static class CompressionAlgorithmExtensions
{
    public static async Task<Stream> CompressAsync(this ICompressionAlgorithm algorithm, Stream stream)
    {
        Guard.AgainstNull(algorithm);
        Guard.AgainstNull(stream);

        using var ms = new MemoryStream();

        await stream.CopyToAsync(ms).ConfigureAwait(false);

        return new MemoryStream(await algorithm.CompressAsync(ms.ToArray()));
    }

    public static async Task<Stream> DecompressAsync(this ICompressionAlgorithm algorithm, Stream stream)
    {
        Guard.AgainstNull(algorithm);
        Guard.AgainstNull(stream);

        using var ms = new MemoryStream();

        await stream.CopyToAsync(ms).ConfigureAwait(false);

        return new MemoryStream(await algorithm.DecompressAsync(ms.ToArray()));
    }
}