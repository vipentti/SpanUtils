// Copyright 2023-2024 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using FluentAssertions.Execution;
using SpanUtils.Extensions;

namespace SpanUtils.Tests;

public class ReadOnlySpanWindowsTests
{
    [Theory]
    [MemberData(nameof(SpanWindowsTests.Windows), MemberType = typeof(SpanWindowsTests))]
    public void Produces_Expected_Windows(int[] data, int windowSize, int[][] expected)
    {
        var enu = data.EnumerateReadOnlyWindows(windowSize);

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
}
