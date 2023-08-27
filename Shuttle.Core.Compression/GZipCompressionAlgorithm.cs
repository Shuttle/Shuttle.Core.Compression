using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression
{
    public class GZipCompressionAlgorithm : ICompressionAlgorithm
    {
        public string Name => "GZip";

        public byte[] Compress(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            using var compressed = MemoryStreamCache.Manager.GetStream();
            using (var gzip = new GZipStream(compressed, CompressionMode.Compress, true))
            {
                gzip.Write(bytes, 0, bytes.Length);
            }

            return compressed.ToArray();
        }

        public byte[] Decompress(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            using var gzip = new GZipStream(new MemoryStream(bytes), CompressionMode.Decompress);
            using var decompressed = MemoryStreamCache.Manager.GetStream();

            gzip.CopyTo(decompressed);

            return decompressed.ToArray();
        }

        public async Task<byte[]> CompressAsync(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            using var compressed = MemoryStreamCache.Manager.GetStream();

            var gzip = new GZipStream(compressed, CompressionMode.Compress, true);
            
            await using (gzip.ConfigureAwait(false))
            {
                await gzip.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
            }

            return compressed.ToArray();
        }

        public async Task<byte[]> DecompressAsync(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            using var decompressed = MemoryStreamCache.Manager.GetStream();

            var gzip = new GZipStream(new MemoryStream(bytes), CompressionMode.Decompress);
            
            await using (gzip.ConfigureAwait(false))
            {
                await gzip.CopyToAsync(decompressed).ConfigureAwait(false);
            }

            return decompressed.ToArray();
        }
    }
}