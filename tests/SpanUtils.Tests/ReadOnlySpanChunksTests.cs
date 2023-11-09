// Copyright 2023 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using FluentAssertions.Execution;
using SpanUtils.Extensions;

namespace SpanUtils.Tests;

public class ReadOnlySpanChunksTests
{
    [Theory]
    [MemberData(nameof(SpanChunksTests.Chunks), MemberType = typeof(SpanChunksTests))]
    public void Produces_Expected_Chunks(int[] data, int chunkSize, bool exact, int[][] expected)
    {
        var enu = data.GetReadOnlyChunksEnumerator(chunkSize, exact);

        using (new AssertionScope())
        {
            foreach (var row in expected)
            {
                enu.MoveNext().Should().BeTrue();
                enu.Current.Span.ToArray().Should().BeEquivalentTo(row);
            }

            enu.MoveNext().Should().BeFalse();

            enu.Reset();

            var index = 0;

            foreach (ReadOnlySpan<int> chunk in enu)
            {
                chunk.ToArray().Should().BeEquivalentTo(expected[index]);
                ++index;
            }

            index.Should().Be(expected.Length);
        }
    }

    [Fact]
    public void ThrowsWhenChunkSizeIsZero()
    {
        FluentActions.Invoking(() =>
        {
            var data = new[] { 0, };
            _ = data.GetReadOnlyChunksEnumerator(0);
        }).Should().ThrowExactly<ArgumentException>()
            .WithMessage("Chunk size must be greater than 0*");
    }
}
