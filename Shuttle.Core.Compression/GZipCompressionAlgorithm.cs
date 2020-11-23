using System.IO;
using System.IO.Compression;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression
{
    public class GZipCompressionAlgorithm : ICompressionAlgorithm
    {
        public string Name => "GZip";

        public byte[] Compress(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            using (var compressed = MemoryStreamCache.Manager.GetStream())
            {
                using (var gzip = new GZipStream(compressed, CompressionMode.Compress, true))
                {
                    gzip.Write(bytes, 0, bytes.Length);
                }

                return compressed.ToArray();
            }
        }

        public byte[] Decompress(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            using (var gzip = new GZipStream(new MemoryStream(bytes), CompressionMode.Decompress))
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