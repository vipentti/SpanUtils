![icon](https://raw.githubusercontent.com/vipentti/SpanUtils/main/icon.png)

# SpanUtils

SpanUtils is a .NET library providing utilities and extensions for working with [System.Span](https://learn.microsoft.com/en-us/dotnet/api/system.span-1)

## Features

- Supports various utility enumerators for enumerating values inside spans

## Installation

To install SpanUtils run:

```sh
dotnet add package SpanUtils
```

## Usage

After installation to utilize the extensions add the following using statement:

```csharp
using SpanUtils.Extensions;
```

Now the various extension methods are available:

```csharp
using SpanUtils.Extensions;
using System.Diagnostics;

int chunkSize = 2;
int[] data = new[] { 0, 1, 2, 3, 4, 5};
int[][] expectedChunks = new[]
{
    new[] { 0, 1 },
    new[] { 2, 3 },
    new[] { 4, 5 },
};

int index = 0;
foreach (Span<int> chunk in data.GetChunksEnumerator(chunkSize, exact: true))
{
    Debug.Assert(chunk.SequenceEqual(expectedChunks[index]));
    ++index;
}
```

For more examples of usage see the tests in [tests/SpanUtils.Tests](./tests/SpanUtils.Tests/)

## License

SpanUtils is licensed under the [MIT License](./LICENSE.md)
