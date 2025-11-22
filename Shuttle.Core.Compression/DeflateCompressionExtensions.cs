using Microsoft.Extensions.DependencyInjection;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression;

public static class DeflateCompressionExtensions
{
    extension(CompressionBuilder compressionBuilder)
    {
        public CompressionBuilder AddDeflate()
        {
            Guard.AgainstNull(compressionBuilder).Services.AddSingleton<ICompressionAlgorithm, DeflateCompressionAlgorithm>();

            return compressionBuilder;
        }
    }
}