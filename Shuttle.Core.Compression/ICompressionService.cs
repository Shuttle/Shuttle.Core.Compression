namespace Shuttle.Core.Compression
{
    public interface ICompressionService
    {
        byte[] Compress(string name, byte[] bytes);
        byte[] Decompress(string name, byte[] bytes);
        ICompressionService Add(ICompressionAlgorithm compressionAlgorithm);
        ICompressionAlgorithm Get(string name);
    }
}