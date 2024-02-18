// Copyright 2023-2024 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using FluentAssertions.Execution;
using SpanUtils.Extensions;

namespace SpanUtils.Tests;

public class SpanChunksTests
{
    [Theory]
    [MemberData(nameof(Chunks))]
    public void Produces_Expected_Chunks(int[] data, int chunkSize, bool exact, int[][] expected)
    {
        using (new AssertionScope())
        {
            var enu = data.EnumerateChunks(chunkSize, exact);
            foreach (var row in expected)
            {
                enu.MoveNext().Should().BeTrue();
                enu.Current.Span.ToArray().Should().BeEquivalentTo(row);
            }

            enu.MoveNext().Should().BeFalse();

            enu.Reset();

            var index = 0;

            foreach (Span<int> chunk in enu)
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
        FluentActions
            .Invoking(() =>
            {
                var data = new[] { 0, };
                _ = data.EnumerateChunks(0);
            })
            .Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage("Chunk size must be greater than 0*");
    }

    public static readonly TheoryData<int[], int, bool, int[][]> Chunks =
        new()
        {
            { new[] { 0, 1, 2, }, 1, true, new[] { new[] { 0, }, new[] { 1, }, new[] { 2, }, } },
            {
                new[] { 0, 1, 2, 3, 4, 5 },
                2,
                true,
                new[] { new[] { 0, 1 }, new[] { 2, 3 }, new[] { 4, 5 }, }
            },
            {
                new[] { 0, 1, 2, 3, 4, 5 },
                3,
                true,
                new[] { new[] { 0, 1, 2 }, new[] { 3, 4, 5 }, }
            },
            {
                new[] { 0, 1, 2, 3, 4, 5, 6 },
                2,
                true,
                new[] { new[] { 0, 1 }, new[] { 2, 3 }, new[] { 4, 5 }, }
            },
            {
                new[] { 0, 1, 2, 3, 4, 5, 6 },
                2,
                false,
                new[] { new[] { 0, 1 }, new[] { 2, 3 }, new[] { 4, 5 }, new[] { 6 }, }
            },
            {
                new[] { 0, 1, 2, 3, 4, 5, 6, 7 },
                3,
                false,
                new[] { new[] { 0, 1, 2 }, new[] { 3, 4, 5 }, new[] { 6, 7 }, }
            },
        };
}
