namespace Shuttle.Core.Compression
{
    public class NullCompressionAlgorithm : ICompressionAlgorithm
    {
        public string Name => "null";

        public byte[] Compress(byte[] bytes)
        {
            return bytes;
        }

        public byte[] Decompress(byte[] bytes)
        {
            return bytes;
        }
    }
}