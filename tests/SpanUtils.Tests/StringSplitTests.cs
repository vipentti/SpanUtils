// Copyright 2023 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using SpanUtils.Extensions;

namespace SpanUtils.Tests;

public class StringSplitTests
{
    public static readonly StringSplitOptions[] SplitOptions = new[]
    {
        StringSplitOptions.None,
        StringSplitOptions.RemoveEmptyEntries,
#if NET5_0_OR_GREATER
        StringSplitOptions.TrimEntries,
        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries,
#endif
    };

    public static TheoryData<string, T, StringSplitOptions> MakeTestCases<T>((string, T)[] input)
    {
        var data = new TheoryData<string, T, StringSplitOptions>();

        foreach (var option in SplitOptions)
        {
            foreach (var (lhs, rhs) in input)
            {
                data.Add(lhs, rhs, option);
            }
        }

        return data;
    }

    public static readonly (string, char)[] CharTestData = new (string, char)[]
    {
        (";", ';'),
        (";;", ';'),
        (";;;", ';'),
        (";;;;;;;;;;;;;;", ';'),
        ("apple, orange, banana, peach", ','),
        ("one;two;three;four", ';'),
        ("hello||world||from||C#", '|'),
        ("2023-04-01T12:30:00", '-'),
        ("2023-04-01T12:30:00", 'T'),
        ("2023-04-01T12:30:00", ':'),
        ("newline\nseparated\nvalues", '\n'),
        ("tabs\tare\tseparated", '\t'),
        ("mixed separators, and; different|lengths", ','),
        ("mixed separators, and; different|lengths", ';'),
        ("mixed separators, and; different|lengths", '|'),
        ("empty;;separators;are;;here;", ';'),
        ("multiple delimiters,,, in a row;;; test", ','),
        ("multiple delimiters,,, in a row;;; test", ';'),
        ("spaces and tabs\tare tricky", ' '),
        ("spaces and tabs\tare tricky", '\t'),
        ("special\ncharacters\r\nand line\rbreaks", '\n'),
        ("special\ncharacters\r\nand line\rbreaks", '\r'),
        ("foo;b4||a4;;bar;;baz;;;b4||a4;buz", ';'),
        ("foo;b4||a4;;bar;;baz;;;b4||a4;buz", '|'),
        (
            "The handsome, energetic, young dog was playing with his smaller, more lethargic litter mate.",
            ','
        ),
        (
            "The handsome, energetic, young dog was playing with his smaller, more lethargic litter mate.",
            '.'
        ),
        ("; ; hello ; world ;; works ; ; again ; ; ;;", ';'),
    };

    public static readonly TheoryData<string, char, StringSplitOptions> CharTestCases =
        MakeTestCases(CharTestData);

    [Theory]
    [MemberData(nameof(CharTestCases))]
    public void CharSplittingWorksAsExpected(string input, char split, StringSplitOptions options)
    {
        var parts = new List<string>();

        foreach (ReadOnlySpan<char> content in input.EnumerateSplitSubstrings(split, options))
        {
            parts.Add(content.ToString());
        }

        var actualSplit = input.Split(new[] { split }, options);

        parts.Should().BeEquivalentTo(actualSplit);
    }

    public static readonly (string, char[])[] CharArrayTestData = new (string, char[])[]
    {
        (";", new[] { ';' }),
        (";;", new[] { ';' }),
        (";;;", new[] { ';' }),
        (";;;;;;;;;;;;;;", new[] { ';' }),
        ("-_aa-_", new[] { '-', '_' }),
        ("apple, orange, banana, peach", new[] { ',' }),
        ("one;two;three;four", new[] { ';' }),
        ("hello||world||from||C#", new[] { '|' }),
        ("2023-04-01T12:30:00", new[] { '-', 'T', ':' }),
        ("newline\nseparated\nvalues", new[] { '\n' }),
        ("tabs\tare\tseparated", new[] { '\t' }),
        ("mixed separators, and; different|lengths", new[] { ',', ';', '|' }),
        ("empty;;separators;are;;here;", new[] { ';' }),
        ("multiple delimiters,,, in a row;;; test", new[] { ',', ';' }),
        ("spaces and tabs\tare tricky", new[] { ' ', '\t' }),
        ("special\ncharacters\r\nand line\rbreaks", new[] { '\n', '\r' }),
        ("\r\nspecial\r\n\ncharacters\r\nand line\rbreaks\n\r\n", new[] { '\n', '\r' }),
        ("empty array\rsplits\n\nby whitespace\tand\fworks", Array.Empty<char>()),
        (
            "x\fand\r\n xx \t\r\f\n\r\n afaf \r\n tes special\ncharacters\r\nand line\rbreaks\r end ",
            Array.Empty<char>()
        ),
        ("foo;b4||a4;;bar;;baz;;;b4||a4;buz", new[] { ';', '|', ';', '|' }),
        (";;foo;b4||a4;;bar;;baz;;;b4||a4;buz|;;||;<", new[] { ';', '|', ';', '|', '<' }),
        (
            "The handsome, energetic, young dog was playing with his smaller, more lethargic litter mate.",
            new[] { ',', '.', '!', '?', ';', ':', ' ' }
        ),
        ("; | ; || hello| ; | | ;|world ;|works ;again| ; || ;", new[] { ';', '|' }),
        ("; ; hello ; world ;; works ; ; again ; ; ;;", new[] { ';' }),
        ("; ; hello ; world ;; works ; ; again ; ; ;;", new[] { ';', ' ' }),
    };

    public static readonly TheoryData<string, char[], StringSplitOptions> CharArrayCases =
        MakeTestCases(CharArrayTestData);

    [Theory]
    [MemberData(nameof(CharArrayCases))]
    public void CharArraySplittingWorksAsExpected(
        string input,
        char[] split,
        StringSplitOptions options
    )
    {
        var parts = new List<string>();

        foreach (ReadOnlySpan<char> content in input.EnumerateSplitSubstrings(split, options))
        {
            parts.Add(content.ToString());
        }

        var actualSplit = input.Split(split, options);

        parts.Should().BeEquivalentTo(actualSplit);
    }

    public static readonly (string, string)[] StringTestData = new (string, string)[]
    {
        (";", ";"),
        (";;", ";"),
        (";;;", ";"),
        (";;;;;;;;;;;;;;", ";"),
        ("apple, orange, banana, peach", ","),
        ("apple, orange, banana, peach", ", "),
        ("apple, orange, banana, peach", " "),
        ("apple, orange, banana, peach", ""),
        ("apple, orange, banana, peach", "is not found"),
        ("apple, orange, banana, peach", "longer than apple, orange, banana, peach"),
        ("one;two;three;four", ";"),
        ("hello||world||from||C#", "|"),
        ("2023-04-01T12:30:00", "-"),
        ("2023-04-01T12:30:00", "T"),
        ("2023-04-01T12:30:00", ":"),
        ("newline\nseparated\nvalues", "\n"),
        ("tabs\tare\tseparated", "\t"),
        ("mixed separators, and; different|lengths", ","),
        ("mixed separators, and; different|lengths", ";"),
        ("mixed separators, and; different|lengths", "|"),
        ("empty;;separators;are;;here;", ";"),
        ("multiple delimiters,,, in a row;;; test", ","),
        ("multiple delimiters,,, in a row;;; test", ";"),
        ("spaces and tabs\tare tricky", " "),
        ("spaces and tabs\tare tricky", "\t"),
        ("special\ncharacters\r\nand line\rbreaks", "\n"),
        ("special\ncharacters\r\nand line\rbreaks", "\r"),
        ("foo;b4||a4;;bar;;baz;;;b4||a4;buz", ";"),
        ("foo;b4||a4;;bar;;baz;;;b4||a4;buz", "|"),
        (
            "The handsome, energetic, young dog was playing with his smaller, more lethargic litter mate.",
            ","
        ),
        (
            "The handsome, energetic, young dog was playing with his smaller, more lethargic litter mate.",
            "."
        ),
        ("; ; hello ; world ;; works ; ; again ; ; ;;", ";"),
    };

    public static readonly TheoryData<string, string, StringSplitOptions> StringTestCases =
        MakeTestCases(StringTestData);

    [Theory]
    [MemberData(nameof(StringTestCases))]
    public void StringSplittingWorksAsExpected(
        string input,
        string split,
        StringSplitOptions options
    )
    {
        var parts = new List<string>();

        foreach (ReadOnlySpan<char> content in input.EnumerateSplitSubstrings(split, options))
        {
            parts.Add(content.ToString());
        }

        var actualSplit = input.Split(new[] { split }, options);

        parts.Should().BeEquivalentTo(actualSplit);
    }

    public static readonly (string, string[])[] StringArrayTestData = new[]
    {
        (";", new[] { ";" }),
        (";;", new[] { ";" }),
        (";;;", new[] { ";" }),
        (";;;;;;;;;;;;;;", new[] { ";" }),
        ("apple, orange, banana, peach", new[] { ", " }),
        ("apple, orange, banana, peach", new[] { "" }),
        ("apple, orange, banana, peach", new[] { "", "", }),
        ("apple, orange, banana, peach", new[] { "", "", "", }),
        ("apple, orange, banana, peach", Array.Empty<string>()),
        ("apple, orange, banana, peach", new[] { "not found" }),
        ("apple, orange, banana, peach", new[] { "longer than apple, orange, banana, peach" }),
        (
            "apple, orange, banana, peach",
            new[] { "longer than apple, orange, banana, peach", ", " }
        ),
        ("one;two;three;four", new[] { ";" }),
        ("hello||world||from||C#", new[] { "||" }),
        ("2023-04-01T12:30:00", new[] { "-", "T", ":" }),
        ("newline\nseparated\nvalues", new[] { "\n" }),
        ("tabs\tare\tseparated", new[] { "\t" }),
        ("mixed separators, and; different|lengths", new[] { ", ", "; ", "|" }),
        ("empty;;separators;are;;here;", new[] { ";" }),
        ("multiple delimiters,,, in a row;;; test", new[] { ",", ";" }),
        ("spaces and tabs\tare tricky", new[] { " ", "\t" }),
        ("special\ncharacters\r\nand line\rbreaks", new[] { "\n", "\r\n", "\r" }),
        (
            "x\fand\r\n xx \t\r\f\n\r\n afaf \r\n tes special\ncharacters\r\nand line\rbreaks\r end ",
            Array.Empty<string>()
        ),
        ("foo;b4||a4;;bar;;baz;;;b4||a4;buz", new[] { ";", "||", ";;", ";;;", "|" }),
        (";;foo;b4||a4;;bar;;baz;;;b4||a4;buz|;;||;<", new[] { ";", "||", ";;", ";;;", "|", "<" }),
        (
            "The handsome, energetic, young dog was playing with his smaller, more lethargic litter mate.",
            new[] { ", ", " " }
        ),
        (
            "The handsome, energetic, young dog was playing with his smaller, more lethargic litter mate.",
            new[] { ",", ".", "!", "?", ";", ":", " " }
        ),
        ("; ; hello ; world ;; works ; ; again ; ; ;;", new[] { ";" }),
        ("; ; hello ; world ;; works ; ; again ; ; ;;", new[] { ";", " " }),
    };

    public static readonly TheoryData<string, string[], StringSplitOptions> TestStringCases =
        MakeTestCases(StringArrayTestData);

    [Theory]
    [MemberData(nameof(TestStringCases))]
    public void StringArraySplittingWorksAsExpected(
        string input,
        string[] split,
        StringSplitOptions options
    )
    {
        var parts = new List<string>();

        foreach (ReadOnlySpan<char> content in input.EnumerateSplitSubstrings(split, options))
        {
            parts.Add(content.ToString());
        }

        var actualSplit = input.Split(split, options);

        parts.Should().BeEquivalentTo(actualSplit);
    }

    public static readonly TheoryData<string> LinesTestCases =
        new()
        {
            "",
            "no lines",
            "x\rand\r\n xx \t\r\n\r\n afaf \r\n tes special\ncharacters\r\nand line\rbreaks\r end",
            "supports\fvarous\rforms\nof\r\nnewlines\u2028and\u0085and\u2029yay",
            @"
this
is a
multi-line
string
",
        };

    [Theory]
    [MemberData(nameof(LinesTestCases))]
    public void StringLineSplittingWorksAsExpected(string input)
    {
        var parts = new List<string>();

        foreach (ReadOnlySpan<char> content in input.EnumerateSplitLines())
        {
            parts.Add(content.ToString());
        }

        var actualSplit = input.Split(Enumerators.StringSplitUtils.NewLineStrings, default);

        parts.Should().BeEquivalentTo(actualSplit);
    }
}
