using System.IO;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Shuttle.Core.Compression.Tests;

[TestFixture]
public class Fixture
{
    [Test]
    public void Should_be_able_to_compress_and_decompress_using_gzip()
    {
        var algorithm = new GZipCompressionAlgorithm();

        const string text = "gzip compression algorithm";

        AssertAlgorithm(algorithm, text);
    }

    private static void AssertAlgorithm(ICompressionAlgorithm algorithm, string text)
    {
        Assert.AreEqual(text,
            Encoding.UTF8.GetString(algorithm.Decompress(algorithm.Compress(Encoding.UTF8.GetBytes(text)))));

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(text));
        using var compressed = algorithm.Compress(stream);
        using var decompressed = algorithm.Decompress(compressed);
        using var decompressedStream = new MemoryStream();

        decompressed.CopyTo(decompressedStream);

        Assert.AreEqual(text, Encoding.UTF8.GetString(decompressedStream.ToArray()));
    }

    [Test]
    public void Should_be_able_to_compress_and_decompress_using_deflate()
    {
        var algorithm = new DeflateCompressionAlgorithm();

        const string text = "deflate compression algorithm";

        AssertAlgorithm(algorithm, text);
    }

    [Test]
    public async Task Should_be_able_to_compress_and_decompress_using_gzip_async()
    {
        var algorithm = new GZipCompressionAlgorithm();

        const string text = "gzip compression algorithm";

        await AssertAlgorithmAsync(algorithm, text);
    }

    private static async Task AssertAlgorithmAsync(ICompressionAlgorithm algorithm, string text)
    {
        Assert.AreEqual(text,
            Encoding.UTF8.GetString(await algorithm.DecompressAsync(await algorithm.CompressAsync(Encoding.UTF8.GetBytes(text)))));

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(text));
        await using var compressed = await algorithm.CompressAsync(stream);
        await using var decompressed = await algorithm.DecompressAsync(compressed);
        using var decompressedStream = new MemoryStream();

        await decompressed.CopyToAsync(decompressedStream);

        Assert.AreEqual(text, Encoding.UTF8.GetString(decompressedStream.ToArray()));
    }

    [Test]
    public async Task Should_be_able_to_compress_and_decompress_using_deflate_async()
    {
        var algorithm = new DeflateCompressionAlgorithm();

        const string text = "deflate compression algorithm";

        await AssertAlgorithmAsync(algorithm, text);
    }
}