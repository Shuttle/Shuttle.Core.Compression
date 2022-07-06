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

```
services.AddCompression();
```

This will try to the `CompressionService` singleton.

In order to add compression options use the relevant calls:

```
services.AddCompression(options => {
	options.AddGzip();
	options.AddDeflate();
});
```
