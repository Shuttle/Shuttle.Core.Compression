using System;
using System.Collections.Generic;
using System.Linq;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression;

public class CompressionService : ICompressionService
{
    private readonly Dictionary<string, ICompressionAlgorithm> _compressionAlgorithms = new();

    public CompressionService(IEnumerable<ICompressionAlgorithm>? compressionAlgorithms = null)
    {
        foreach (var compressionAlgorithm in compressionAlgorithms ?? Enumerable.Empty<ICompressionAlgorithm>())
        {
            Add(compressionAlgorithm);
        }
    }

    public ICompressionService Add(ICompressionAlgorithm compressionAlgorithm)
    {
        Guard.AgainstNull(compressionAlgorithm);

        if (!_compressionAlgorithms.TryAdd(compressionAlgorithm.Name, compressionAlgorithm))
        {
            throw new ArgumentException(string.Format(Resources.DuplicateCompressionAlgorithmException, compressionAlgorithm.Name));
        }

        return this;
    }

    public ICompressionAlgorithm Get(string name)
    {
        Guard.AgainstNullOrEmptyString(name);

        if (!_compressionAlgorithms.TryGetValue(name, out var algorithm))
        {
            throw new ArgumentException(string.Format(Resources.CompressionAlgorithmMissingException, name));
        }

        return algorithm;
    }

    public bool Contains(string name)
    {
        return _compressionAlgorithms.ContainsKey(Guard.AgainstNullOrEmptyString(name));
    }

    public IEnumerable<ICompressionAlgorithm> Algorithms => _compressionAlgorithms.Values;
}