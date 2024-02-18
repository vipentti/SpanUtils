// Copyright 2023-2024 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using System;

namespace SpanUtils.Enumerators;

internal static class StringSplitUtils
{
    public static bool TryMoveNext(
        scoped ref ReadOnlySpan<char> remaining,
        scoped ref bool yieldEmpty,
        int index,
        int stride,
        StringSplitOptions options,
        out ReadOnlySpan<char> next
    )
    {
        if (yieldEmpty)
        {
            yieldEmpty = false;
            next = default;
            return true;
        }

        var span = remaining;
        var len = span.Length;

        if (len == 0)
        {
            next = default;
            remaining = default;
            return false;
        }

        ReadOnlySpan<char> content;

#pragma warning disable IDE0057 // Use range operator
        if (index == -1)
        {
            content = span;
            remaining = default;
        }
        else
        {
            content = span.Slice(0, index);
            remaining = span.Slice(index + stride);
            // Check if we have a trailing separator
            if (remaining.Length == 0)
            {
                var separator = span.Slice(index, stride);
                yieldEmpty = separator.Length > 0;
            }
        }
#pragma warning restore IDE0057 // Use range operator

#if NET5_0_OR_GREATER
        if (options.HasFlag(StringSplitOptions.TrimEntries))
        {
            content = content.Trim();
        }
#else
        _ = options;
#endif

        next = content;
        return true;
    }

    public static readonly string[] EmptyStringArray = [];

    public static readonly char[] NewlineChars =
    [
        '\n', // LINE FEED (U+000A)
        '\r', // CARRIAGE RETURN (U+000D)
        '\f', // FORM FEED (U+000C)
        '\u2028', // LINE SEPARATOR (U+2028)
        '\u0085', // NEXT LINE (U+0085)
        '\u2029', // PARAGRAPH SEPARATOR (U+2029)
    ];

    public static readonly string[] NewLineStrings =
    [
        "\r\n", // CARRIAGE RETURN (U+000D) + LINE FEED (U+000A)
        "\n", // LINE FEED (U+000A)
        "\r", // CARRIAGE RETURN (U+000D)
        "\f", // FORM FEED (U+000C)
        "\u2028", // LINE SEPARATOR (U+2028)
        "\u0085", // NEXT LINE (U+0085)
        "\u2029", // PARAGRAPH SEPARATOR (U+2029)
    ];

    public static readonly string[] WhitespaceStrings =
    [
        " ", // SPACE (U+0020)
        "\u00A0", // NO-BREAK SPACE (U+00A0)
        "\u1680", // OGHAM SPACE MARK (U+1680)
        "\u2000", // EN QUAD (U+2000)
        "\u2001", // EM QUAD (U+2001)
        "\u2002", // EN SPACE (U+2002)
        "\u2003", // EM SPACE (U+2003)
        "\u2004", // THREE-PER-EM SPACE (U+2004)
        "\u2005", // FOUR-PER-EM SPACE (U+2005)
        "\u2006", // SIX-PER-EM SPACE (U+2006)
        "\u2007", // FIGURE SPACE (U+2007)
        "\u2008", // PUNCTUATION SPACE (U+2008)
        "\u2009", // THIN SPACE (U+2009)
        "\u200A", // HAIR SPACE (U+200A)
        "\u2028", // LINE SEPARATOR (U+2028)
        "\u2029", // PARAGRAPH SEPARATOR (U+2029)
        "\u202F", // NARROW NO-BREAK SPACE (U+202F)
        "\u205F", // MEDIUM MATHEMATICAL SPACE (U+205F)
        "\u3000", // IDEOGRAPHIC SPACE (U+3000)
        "\t", // CHARACTER TABULATION (U+0009)
        "\n", // LINE FEED (U+000A)
        "\u000B", // LINE TABULATION (U+000B)
        "\f", // FORM FEED (U+000C)
        "\r", // CARRIAGE RETURN (U+000D)
        "\u0085", // NEXT LINE (U+0085)
    ];

    // https://learn.microsoft.com/en-us/dotnet/api/system.char.iswhitespace?view=net-7.0#remarks
    public static readonly char[] WhitespaceChars =
    [
        ' ', // SPACE (U+0020)
        '\u00A0', // NO-BREAK SPACE (U+00A0)
        '\u1680', // OGHAM SPACE MARK (U+1680)
        '\u2000', // EN QUAD (U+2000)
        '\u2001', // EM QUAD (U+2001)
        '\u2002', // EN SPACE (U+2002)
        '\u2003', // EM SPACE (U+2003)
        '\u2004', // THREE-PER-EM SPACE (U+2004)
        '\u2005', // FOUR-PER-EM SPACE (U+2005)
        '\u2006', // SIX-PER-EM SPACE (U+2006)
        '\u2007', // FIGURE SPACE (U+2007)
        '\u2008', // PUNCTUATION SPACE (U+2008)
        '\u2009', // THIN SPACE (U+2009)
        '\u200A', // HAIR SPACE (U+200A)
        '\u2028', // LINE SEPARATOR (U+2028)
        '\u2029', // PARAGRAPH SEPARATOR (U+2029)
        '\u202F', // NARROW NO-BREAK SPACE (U+202F)
        '\u205F', // MEDIUM MATHEMATICAL SPACE (U+205F)
        '\u3000', // IDEOGRAPHIC SPACE (U+3000)
        '\t', // CHARACTER TABULATION (U+0009)
        '\n', // LINE FEED (U+000A)
        '\u000B', // LINE TABULATION (U+000B)
        '\f', // FORM FEED (U+000C)
        '\r', // CARRIAGE RETURN (U+000D)
        '\u0085', // NEXT LINE (U+0085)
    ];
}
