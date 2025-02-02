using System.Collections.Generic;

namespace Shuttle.Core.Compression;

public interface ICompressionService
{
    IEnumerable<ICompressionAlgorithm> Algorithms { get; }
    ICompressionService Add(ICompressionAlgorithm compressionAlgorithm);
    bool Contains(string name);
    ICompressionAlgorithm Get(string name);
}