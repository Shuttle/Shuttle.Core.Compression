using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression
{
    public class DeflateCompressionAlgorithm : ICompressionAlgorithm
    {
        public string Name => "Deflate";

        public byte[] Compress(byte[] bytes)
        {
            return CompressAsync(bytes, true).GetAwaiter().GetResult();
        }

        public byte[] Decompress(byte[] bytes)
        {
            return DecompressAsync(bytes, true).GetAwaiter().GetResult();
        }

        public async Task<byte[]> CompressAsync(byte[] bytes)
        {
            return await CompressAsync(bytes, false);
        }

        private static async Task<byte[]> CompressAsync(byte[] bytes, bool sync)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            using var compressed = MemoryStreamCache.Manager.GetStream();
            await using var deflate = new DeflateStream(compressed, CompressionMode.Compress, true);

            if (sync)
            {
                deflate.Write(bytes, 0, bytes.Length);
            }
            else
            {
                await deflate.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
            }

            deflate.Flush();

            return compressed.ToArray();
        }

        public async Task<byte[]> DecompressAsync(byte[] bytes)
        {
            return await DecompressAsync(bytes, false);
        }

        private static async Task<byte[]> DecompressAsync(byte[] bytes, bool sync)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            using var decompressed = MemoryStreamCache.Manager.GetStream();

            var deflate = new DeflateStream(new MemoryStream(bytes), CompressionMode.Decompress);
            
            await using (deflate.ConfigureAwait(false))
            {
                if (sync)
                {
                    deflate.CopyTo(decompressed);
                }
                else
                {
                    await deflate.CopyToAsync(decompressed).ConfigureAwait(false);
                }
            }

            return decompressed.ToArray();
        }
    }
}