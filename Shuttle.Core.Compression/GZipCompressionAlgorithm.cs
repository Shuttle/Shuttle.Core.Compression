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
            return CompressAsync(bytes, true).GetAwaiter().GetResult();
        }

        public async Task<byte[]> CompressAsync(byte[] bytes)
        {
            return await CompressAsync(bytes, false).ConfigureAwait(false);
        }

        private static async Task<byte[]> CompressAsync(byte[] bytes, bool sync)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            using var compressed = MemoryStreamCache.Manager.GetStream();

            var gzip = new GZipStream(compressed, CompressionMode.Compress, true);
            
            await using (gzip.ConfigureAwait(false))
            {
                if (sync)
                {
                    gzip.Write(bytes, 0, bytes.Length);
                }
                else
                {
                    await gzip.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
                }
            }

            return compressed.ToArray();
        }

        public byte[] Decompress(byte[] bytes)
        {
            return DecompressAsync(bytes, true).GetAwaiter().GetResult();
        }

        public async Task<byte[]> DecompressAsync(byte[] bytes)
        {
            return await DecompressAsync(bytes, false).ConfigureAwait(false);
        }

        private static async Task<byte[]> DecompressAsync(byte[] bytes, bool sync)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            using var decompressed = MemoryStreamCache.Manager.GetStream();

            var gzip = new GZipStream(new MemoryStream(bytes), CompressionMode.Decompress);
            
            await using (gzip.ConfigureAwait(false))
            {
                if (sync)
                {
                    gzip.CopyTo(decompressed);
                }
                else
                {
                    await gzip.CopyToAsync(decompressed).ConfigureAwait(false);
                }
            }

            return decompressed.ToArray();
        }
    }
}