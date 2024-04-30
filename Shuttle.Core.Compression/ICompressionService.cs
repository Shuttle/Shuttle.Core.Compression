using System.Collections;
using System.Collections.Generic;

namespace Shuttle.Core.Compression
{
    public interface ICompressionService
    {
        ICompressionService Add(ICompressionAlgorithm compressionAlgorithm);
        ICompressionAlgorithm Get(string name);
        bool Contains(string name);
        IEnumerable<ICompressionAlgorithm> Algorithms { get; }
    }
}