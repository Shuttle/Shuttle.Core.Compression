using System;
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

            var resultStream = MemoryStreamCache.Get();
            using (var gzip = new GZipStream(resultStream, CompressionMode.Compress, true))
            {
                gzip.Write(bytes, 0, bytes.Length);
            }

            var result = resultStream.ToArray();
            MemoryStreamCache.Return(resultStream);

            return result;
        }

        public byte[] Decompress(byte[] bytes)
        {
            Guard.AgainstNull(bytes, nameof(bytes));

            using (var gzip = new GZipStream(new MemoryStream(bytes), CompressionMode.Decompress))
            {
                var resultStream = MemoryStreamCache.Get();
                gzip.CopyTo(resultStream);
                
                var result = resultStream.ToArray();
                MemoryStreamCache.Return(resultStream);

                return result;
            }
        }
    }
}