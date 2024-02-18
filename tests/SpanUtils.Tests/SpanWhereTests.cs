// Copyright 2023-2024 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using SpanUtils.Extensions;

namespace SpanUtils.Tests;

public class SpanWhereTests
{
    [Theory]
    [MemberData(
        nameof(ReadOnlySpanWhereTests.PredicateTestCases),
        MemberType = typeof(ReadOnlySpanWhereTests)
    )]
    public void YieldsValuesMatchingPredicate(int[] data, Predicate<int> predicate, int[] expected)
    {
        var result = new List<int>();

        foreach (var item in data.EnumerateWhere(predicate))
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

        var enu = data.EnumerateWhere(it => it > 0);

        while (enu.MoveNext())
        {
            ref var value = ref enu.Current;
            ++value;
        }

        data.Should().BeEquivalentTo(new[] { 0, 2, 3, 4, });
    }

    [Fact]
    public void CanUseRefsToMutateForeach()
    {
        var data = new[] { 0, 1, 2, 3 };

        foreach (ref var value in data.EnumerateWhere(it => it > 0))
        {
            ++value;
        }

        data.Should().BeEquivalentTo(new[] { 0, 2, 3, 4, });
    }
#endif
}
