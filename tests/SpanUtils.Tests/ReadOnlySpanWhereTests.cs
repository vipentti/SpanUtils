// Copyright 2023-2024 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using SpanUtils.Extensions;

namespace SpanUtils.Tests;

public class ReadOnlySpanWhereTests
{
    [Theory]
    [MemberData(nameof(PredicateTestCases))]
    public void YieldsValuesMatchingPredicate(int[] data, Predicate<int> predicate, int[] expected)
    {
        var result = new List<int>();

        foreach (var item in data.EnumerateReadOnlyWhere(predicate))
        {
            result.Add(item);
        }

        result.Should().BeEquivalentTo(expected);
    }

    public static readonly TheoryData<int[], Predicate<int>, int[]> PredicateTestCases =
        new()
        {
            { Array.Empty<int>(), _ => false, Array.Empty<int>() },
            { new[] { 0, 1, 2 }, it => it >= 1, new[] { 1, 2 } },
            { new[] { 3, 4, 5 }, it => it == 4, new[] { 4 } },
            { new[] { 0, 1, 2 }, _ => true, new[] { 0, 1, 2 } },
        };
}
