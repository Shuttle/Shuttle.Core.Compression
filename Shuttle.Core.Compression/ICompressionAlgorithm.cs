namespace Shuttle.Core.Compression
{
    public interface ICompressionAlgorithm
    {
        string Name { get; }

        byte[] Compress(byte[] bytes);

        byte[] Decompress(byte[] bytes);
    }
}