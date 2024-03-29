﻿// Copyright 2023-2024 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using SpanUtils.Extensions;

namespace SpanUtils.Tests;

public class ReadOnlySpanSelectTests
{
    public const string AddOne = "AddOne";

    public static readonly Dictionary<string, Func<int, int>> Methods =
        new() { [AddOne] = it => it + 1, };

    [Theory]
    [MemberData(nameof(Selections), MemberType = typeof(ReadOnlySpanSelectTests))]
    public void PerformsSelections(int[] data, string method, int[] expected)
    {
        var result = new List<int>();

        foreach (var item in data.EnumerateReadOnlySelect(Methods[method]))
        {
            result.Add(item);
        }

        result.Should().BeEquivalentTo(expected);
    }

    public static readonly TheoryData<int[], string, int[]> Selections =
        new()
        {
            { new[] { 0, 1, 2 }, AddOne, new[] { 1, 2, 3 } },
            { new[] { 3, 4, 5 }, AddOne, new[] { 4, 5, 6 } },
        };
}
