using Microsoft.Extensions.DependencyInjection;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression;

public static class GZipCompressionExtensions
{
    extension(CompressionBuilder compressionBuilder)
    {
        public CompressionBuilder AddGZip()
        {
            Guard.AgainstNull(compressionBuilder).Services.AddSingleton<ICompressionAlgorithm, GZipCompressionAlgorithm>();

            return compressionBuilder;
        }
    }
}