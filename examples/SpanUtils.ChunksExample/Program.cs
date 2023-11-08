using SpanUtils.Extensions;
using System.Diagnostics;

int chunkSize = 2;
int[] data = new[] { 0, 1, 2, 3, 4, 5 };
int[][] expectedChunks = new[]
{
    new[] { 0, 1 },
    new[] { 2, 3 },
    new[] { 4, 5 },
};

{
    int index = 0;
    foreach (Span<int> chunk in data.GetChunksEnumerator(chunkSize, exact: true))
    {
        Debug.Assert(chunk.SequenceEqual(expectedChunks[index]));
        ++index;
    }
}
{
    int index = 0;
    foreach (ReadOnlySpan<int> chunk in data.GetReadOnlyChunksEnumerator(chunkSize, exact: true))
    {
        Debug.Assert(chunk.SequenceEqual(expectedChunks[index]));
        ++index;
    }
}
{
    Span<int> dataSpan = data;
    int index = 0;
    foreach (ReadOnlySpan<int> chunk in dataSpan.GetReadOnlyChunksEnumerator(chunkSize, exact: true))
    {
        Debug.Assert(chunk.SequenceEqual(expectedChunks[index]));
        ++index;
    }
}
{
    ReadOnlySpan<int> readOnlyDataSpan = data;
    int index = 0;
    foreach (ReadOnlySpan<int> chunk in readOnlyDataSpan.GetReadOnlyChunksEnumerator(chunkSize, exact: true))
    {
        Debug.Assert(chunk.SequenceEqual(expectedChunks[index]));
        ++index;
    }
}
