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
