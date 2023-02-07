namespace Shuttle.Core.Compression
{
    public interface ICompressionService
    {
        ICompressionService Add(ICompressionAlgorithm compressionAlgorithm);
        ICompressionAlgorithm Get(string name);
    }
}