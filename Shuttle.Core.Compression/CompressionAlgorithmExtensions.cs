using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression;

public static class CompressionAlgorithmExtensions
{
    extension(ICompressionAlgorithm compressionAlgorithm)
    {
        public async Task<Stream> CompressAsync(Stream stream)
        {
            Guard.AgainstNull(compressionAlgorithm);
            Guard.AgainstNull(stream);

            using var ms = new MemoryStream();

            await stream.CopyToAsync(ms).ConfigureAwait(false);

            return new MemoryStream(await compressionAlgorithm.CompressAsync(ms.ToArray()));
        }

        public async Task<Stream> DecompressAsync(Stream stream)
        {
            Guard.AgainstNull(compressionAlgorithm);
            Guard.AgainstNull(stream);

            using var ms = new MemoryStream();

            await stream.CopyToAsync(ms).ConfigureAwait(false);

            return new MemoryStream(await compressionAlgorithm.DecompressAsync(ms.ToArray()));
        }
    }
}