using System;
using System.IO;

namespace Shuttle.Core.Compression
{
    internal static class MemoryStreamCache
    {
        private const int KeepMemoryStreamLimit = 4096;

        [ThreadStatic]
        private static MemoryStream _cachedStream;

        public static MemoryStream Get()
        {
            MemoryStream stream;

            if (_cachedStream == null)
            {
                stream = new MemoryStream();
            }
            else
            {
                stream = _cachedStream;
                _cachedStream = null;
            }

            return stream;
        }

        public static void Return(MemoryStream stream)
        {
            if (stream.Capacity <= KeepMemoryStreamLimit)
            {
                stream.SetLength(0);
                _cachedStream = stream;
            }
        }
    }
}