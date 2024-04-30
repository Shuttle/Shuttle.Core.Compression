using System;
using System.Collections.Generic;
using System.Linq;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Compression
{
    public class CompressionService : ICompressionService
    {
        private readonly Dictionary<string, ICompressionAlgorithm> _compressionAlgorithms = new Dictionary<string, ICompressionAlgorithm>();

        public CompressionService(IEnumerable<ICompressionAlgorithm> compressionAlgorithms = null)
        {
            foreach (var compressionAlgorithm in compressionAlgorithms ?? Enumerable.Empty<ICompressionAlgorithm>())
            {
                Add(compressionAlgorithm);
            }
        }

        public ICompressionService Add(ICompressionAlgorithm compressionAlgorithm)
        {
            Guard.AgainstNull(compressionAlgorithm, nameof(compressionAlgorithm));

            if (_compressionAlgorithms.ContainsKey(compressionAlgorithm.Name))
            {
                throw new ArgumentException(string.Format(Resources.DuplicateCompressionAlgorithmException,
                    compressionAlgorithm.Name));
            }

            _compressionAlgorithms.Add(compressionAlgorithm.Name, compressionAlgorithm);

            return this;
        }

        public ICompressionAlgorithm Get(string name)
        {
            Guard.AgainstNullOrEmptyString(name, nameof(name));

            if (!_compressionAlgorithms.ContainsKey(name))
            {
                throw new ArgumentException(string.Format(Resources.CompressionAlgorithmMissingException, name));
            }

            return _compressionAlgorithms[name];
        }

        public bool Contains(string name)
        {
            return _compressionAlgorithms.ContainsKey(Guard.AgainstNullOrEmptyString(name, nameof(name)));
        }

        public IEnumerable<ICompressionAlgorithm> Algorithms => _compressionAlgorithms.Values;
    }
}