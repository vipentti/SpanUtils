// Copyright 2023 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using FluentAssertions.Execution;
using SpanUtils.Extensions;

namespace SpanUtils.Tests;

public class SpanWindowsTests
{
    [Theory]
    [MemberData(nameof(Windows))]
    public void Produces_Expected_Windows(int[] data, int windowSize, int[][] expected)
    {
        var enu = data.EnumerateWindows(windowSize);

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

    public static readonly TheoryData<int[], int, int[][]> Windows =
        new()
        {
            {
                new[] { 0, 1, 2, },
                0,
                new[]
                {
                    Array.Empty<int>(),
                    Array.Empty<int>(),
                    Array.Empty<int>(),
                    Array.Empty<int>(),
                }
            },
            { new[] { 0, 1, 2, }, 1, new[] { new[] { 0, }, new[] { 1, }, new[] { 2, }, } },
            {
                new[] { 0, 1, 2, 3, 4, 5 },
                2,
                new[]
                {
                    new[] { 0, 1 },
                    new[] { 1, 2 },
                    new[] { 2, 3 },
                    new[] { 3, 4 },
                    new[] { 4, 5 },
                }
            },
            {
                new[] { 0, 1, 2, 3, 4, 5 },
                3,
                new[]
                {
                    new[] { 0, 1, 2 },
                    new[] { 1, 2, 3 },
                    new[] { 2, 3, 4 },
                    new[] { 3, 4, 5 },
                }
            },
        };
}
