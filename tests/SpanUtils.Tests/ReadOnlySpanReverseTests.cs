// Copyright 2023-2024 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using SpanUtils.Extensions;

namespace SpanUtils.Tests;

public class ReadOnlySpanReverseTests
{
    [Theory]
    [MemberData(nameof(SpanReverseTests.Reverse), MemberType = typeof(SpanReverseTests))]
    public void Enumerates_In_Reverse(int[] data, int[] expected)
    {
        var result = new List<int>();

        foreach (var item in data.EnumerateReadOnlyReverse())
        {
            result.Add(item);
        }

        result.Should().BeEquivalentTo(expected);
    }
}
