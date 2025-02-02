using Microsoft.Extensions.DependencyInjection;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression;

public static class DeflateCompressionExtensions
{
    public static CompressionBuilder AddDeflate(this CompressionBuilder builder)
    {
        Guard.AgainstNull(builder).Services.AddSingleton<ICompressionAlgorithm, DeflateCompressionAlgorithm>();

        return builder;
    }
}