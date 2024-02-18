// Copyright 2023-2024 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using SpanUtils.Extensions;

namespace SpanUtils.Tests;

public class SpanReverseTests
{
    [Theory]
    [MemberData(nameof(SpanReverseTests.Reverse), MemberType = typeof(SpanReverseTests))]
    public void Enumerates_In_Reverse(int[] data, int[] expected)
    {
        var result = new List<int>();

        foreach (var item in data.EnumerateReverse())
        {
            result.Add(item);
        }

        result.Should().BeEquivalentTo(expected);
    }

#if NET7_0_OR_GREATER
    [Fact]
    public void CanUseRefsToMutate()
    {
        var data = new[] { 0, 1, 2, 3 };

        var enu = data.EnumerateReverse();

        while (enu.MoveNext())
        {
            ref var valueref = ref enu.Current;
            valueref++;
        }

        data.Should().BeEquivalentTo(new[] { 1, 2, 3, 4, });
    }

    [Fact]
    public void CanUseRefsToMutateForeach()
    {
        var data = new[] { 0, 1, 2, 3 };

        foreach (ref var valueref in data.EnumerateReverse())
        {
            valueref++;
        }

        data.Should().BeEquivalentTo(new[] { 1, 2, 3, 4, });
    }
#endif

    public static readonly TheoryData<int[], int[]> Reverse =
        new()
        {
            { Array.Empty<int>(), Array.Empty<int>() },
            { new int[] { 0 }, new int[] { 0 } },
            { new int[] { 0, 1 }, new int[] { 1, 0 } },
            { new[] { 0, 1, 2, }, new[] { 2, 1, 0, } },
        };
}
