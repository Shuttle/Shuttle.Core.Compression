# Shuttle.Core.Compression

```
PM> Install-Package Shuttle.Core.Compression
```

Provides a compression adapter through the `ICompressionAlgorithm` interface.

Implementations available in this package:

- `DeflateCompressionAlgorithm`
- `GZipCompressionAlgorithm`
- `NullCompressionAlgorithm`

There is also an `ICompressionService` that acts as a central container for all registered `ICompressionAlgorithm` implementations.

## Configuration

In order to add compression:

```c#
services.AddCompression();
```

This will try to add the `CompressionService` singleton.

In order to add specific compression algorithms use the relevant builder calls:

```c#
services.AddCompression(builder => {
	builder.AddGzip();
	builder.AddDeflate();
	builder.AddNull();
});
```

## Usage

The `ICompressionService` can be injected into any class that requires compression services:

```c#
var algorithm = compressionService.Get("algorithm-name");
var compressed = await algorithm.CompressAsync(Encoding.UTF8.GetBytes("some data"));
var decompressed = await algorithm.DecompressAsync(compressed);
```