using Microsoft.IO;

namespace Shuttle.Core.Compression;

internal static class MemoryStreamCache
{
    public static readonly RecyclableMemoryStreamManager Manager = new();
}