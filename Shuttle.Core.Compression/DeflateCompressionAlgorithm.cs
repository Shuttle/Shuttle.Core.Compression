using System.IO;
using System.IO.Compression;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression
{
    public class DeflateCompressionAlgorithm : ICompressionAlgorithm
    {
        public string Name => "Deflate";

        public byte[] Compress(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            using (var compressed = MemoryStreamCache.Manager.GetStream())
            {
                using (var gzip = new DeflateStream(compressed, CompressionMode.Compress, true))
                {
                    gzip.Write(bytes, 0, bytes.Length);
                }

                return compressed.ToArray();
            }
        }

        public byte[] Decompress(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            using (var gzip = new DeflateStream(new MemoryStream(bytes), CompressionMode.Decompress))
            {
                using (var decompressed = MemoryStreamCache.Manager.GetStream())
                {
                    gzip.CopyTo(decompressed);
                    return decompressed.ToArray();
                }
            }
        }
    }
}