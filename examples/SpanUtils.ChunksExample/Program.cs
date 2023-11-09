// Copyright 2023 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using System.Diagnostics;
using SpanUtils.Extensions;

int chunkSize = 2;
int[] data = new[] { 0, 1, 2, 3, 4, 5 };
int[][] expectedChunks = new[] { new[] { 0, 1 }, new[] { 2, 3 }, new[] { 4, 5 }, };


{
    int index = 0;
    foreach (Span<int> chunk in data.EnumerateChunks(chunkSize, exact: true))
    {
        Debug.Assert(chunk.SequenceEqual(expectedChunks[index]));
        ++index;
    }
}

{
    int index = 0;
    foreach (ReadOnlySpan<int> chunk in data.EnumerateReadOnlyChunks(chunkSize, exact: true))
    {
        Debug.Assert(chunk.SequenceEqual(expectedChunks[index]));
        ++index;
    }
}

{
    Span<int> dataSpan = data;
    int index = 0;
    foreach (
        ReadOnlySpan<int> chunk in dataSpan.EnumerateReadOnlyChunks(chunkSize, exact: true)
    )
    {
        Debug.Assert(chunk.SequenceEqual(expectedChunks[index]));
        ++index;
    }
}

{
    ReadOnlySpan<int> readOnlyDataSpan = data;
    int index = 0;
    foreach (
        ReadOnlySpan<int> chunk in readOnlyDataSpan.EnumerateReadOnlyChunks(
            chunkSize,
            exact: true
        )
    )
    {
        Debug.Assert(chunk.SequenceEqual(expectedChunks[index]));
        ++index;
    }
}
