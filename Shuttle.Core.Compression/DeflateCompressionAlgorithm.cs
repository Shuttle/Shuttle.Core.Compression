using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression
{
    public class DeflateCompressionAlgorithm : ICompressionAlgorithm
    {
        public string Name => "Deflate";

        public async Task<byte[]> Compress(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            using var compressed = MemoryStreamCache.Manager.GetStream();

            var gzip = new DeflateStream(compressed, CompressionMode.Compress, true);
            
            await using (gzip.ConfigureAwait(false))
            {
                await gzip.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
            }

            return compressed.ToArray();
        }

        public async Task<byte[]> Decompress(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            using var decompressed = MemoryStreamCache.Manager.GetStream();

            var gzip = new DeflateStream(new MemoryStream(bytes), CompressionMode.Decompress);
            
            await using var _ = gzip.ConfigureAwait(false);
            await gzip.CopyToAsync(decompressed).ConfigureAwait(false);

            return decompressed.ToArray();
        }
    }
}