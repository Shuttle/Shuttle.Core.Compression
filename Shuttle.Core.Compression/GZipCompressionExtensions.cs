using Microsoft.Extensions.DependencyInjection;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression
{
    public static class GZipCompressionExtensions
    {
        public static CompressionBuilder AddGzip(this CompressionBuilder builder)
        {
            Guard.AgainstNull(builder, nameof(builder)).Services.AddSingleton<ICompressionAlgorithm, GZipCompressionAlgorithm>();

            return builder;
        }
    }
}