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
            Guard.AgainstNull(bytes, nameof(bytes));

            using var compressed = MemoryStreamCache.Manager.GetStream();
            using (var deflate = new DeflateStream(compressed, CompressionMode.Compress, true))
            {
                deflate.Write(bytes, 0, bytes.Length);
            }

            return compressed.ToArray();
        }

        public byte[] Decompress(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            using var deflate = new DeflateStream(new MemoryStream(bytes), CompressionMode.Decompress);
            using var decompressed = MemoryStreamCache.Manager.GetStream();

            deflate.CopyTo(decompressed);

            return decompressed.ToArray();
        }

        public async Task<byte[]> CompressAsync(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            using var compressed = MemoryStreamCache.Manager.GetStream();

            var deflate = new DeflateStream(compressed, CompressionMode.Compress, true);
            
            await using (deflate.ConfigureAwait(false))
            {
                await deflate.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
            }

            return compressed.ToArray();
        }

        public async Task<byte[]> DecompressAsync(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            using var decompressed = MemoryStreamCache.Manager.GetStream();

            var deflate = new DeflateStream(new MemoryStream(bytes), CompressionMode.Decompress);
            
            await using (deflate.ConfigureAwait(false))
            {
                await deflate.CopyToAsync(decompressed).ConfigureAwait(false);
            }

            return decompressed.ToArray();
        }
    }
}