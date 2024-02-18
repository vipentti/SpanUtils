// Copyright 2023-2024 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using System;
using SpanUtils.Enumerators;

namespace SpanUtils.Extensions;

/// <summary>
/// Extension methods for <see cref="string"/>, <see cref="ReadOnlySpan{Char}"/> and <see cref="Span{Char}"/>
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Returns an enumeration of lines in the provided input.
    /// </summary>
    /// <param name="input">Input containing lines to enumerate.</param>
    /// <returns>An enumeration of lines.</returns>
    public static StringSplitByLinesEnumerator EnumerateSplitLines(this string input) =>
        new(input.AsSpan());

    /// <inheritdoc cref="EnumerateSplitLines(string)"/>
    public static StringSplitByLinesEnumerator EnumerateSplitLines(this ReadOnlySpan<char> input) =>
        new(input);

    /// <inheritdoc cref="EnumerateSplitLines(string)"/>
    public static StringSplitByLinesEnumerator EnumerateSplitLines(this Span<char> input) =>
        new(input);

    #region Char
    /// <summary>
    /// Returns an enumeration of substrings separated by specified separator and based on the given options.
    /// </summary>
    /// <param name="input">Input to enumerate.</param>
    /// <param name="separator">A character that delimits the substrings in this string.</param>
    /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim substrings and include empty substrings.</param>
    /// <returns>An enumeration of substrings that are delimited by <paramref name="separator"/></returns>
    public static StringSplitByCharEnumerator EnumerateSplitSubstrings(
        this string input,
        char separator,
        StringSplitOptions options
    ) => new(input.AsSpan(), separator, options);

    /// <inheritdoc cref="EnumerateSplitSubstrings(string, char, StringSplitOptions)"/>
    public static StringSplitByCharEnumerator EnumerateSplitSubstrings(
        this ReadOnlySpan<char> input,
        char separator,
        StringSplitOptions options
    ) => new(input, separator, options);

    /// <inheritdoc cref="EnumerateSplitSubstrings(string, char, StringSplitOptions)"/>
    public static StringSplitByCharEnumerator EnumerateSplitSubstrings(
        this Span<char> input,
        char separator,
        StringSplitOptions options
    ) => new(input, separator, options);
    #endregion

    #region CharArray
    /// <summary>
    /// Returns an enumeration of substrings separated by specified separator and based on the given options.
    /// </summary>
    /// <param name="input">Input to enumerate.</param>
    /// <param name="separator">An array of characters that delimit the substrings in this string or an empty array.</param>
    /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim substrings and include empty substrings.</param>
    /// <returns>An enumeration of substrings that are delimited by one or more characters in <paramref name="separator"/></returns>
    public static StringSplitByCharArrayEnumerator EnumerateSplitSubstrings(
        this string input,
        char[] separator,
        StringSplitOptions options
    ) => new(input.AsSpan(), separator, options);

    /// <inheritdoc cref="EnumerateSplitSubstrings(string, char[], StringSplitOptions)"/>
    public static StringSplitByCharArrayEnumerator EnumerateSplitSubstrings(
        this ReadOnlySpan<char> input,
        char[] separator,
        StringSplitOptions options
    ) => new(input, separator, options);

    /// <inheritdoc cref="EnumerateSplitSubstrings(string, char[], StringSplitOptions)"/>
    public static StringSplitByCharArrayEnumerator EnumerateSplitSubstrings(
        this Span<char> input,
        char[] separator,
        StringSplitOptions options
    ) => new(input, separator, options);
    #endregion

    #region String
    /// <summary>
    /// Returns an enumeration of substrings separated by specified separator and based on the given options.
    /// </summary>
    /// <param name="input">Input to enumerate.</param>
    /// <param name="separator">A string that delimits the substrings in this string.</param>
    /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim substrings and include empty substrings.</param>
    /// <returns>An enumeration of substrings that are delimited by <paramref name="separator"/></returns>
    public static StringSplitByStringEnumerator EnumerateSplitSubstrings(
        this string input,
        string separator,
        StringSplitOptions options
    ) => new(input.AsSpan(), separator, options);

    /// <inheritdoc cref="EnumerateSplitSubstrings(string, string, StringSplitOptions)"/>
    public static StringSplitByStringEnumerator EnumerateSplitSubstrings(
        this ReadOnlySpan<char> input,
        string separator,
        StringSplitOptions options
    ) => new(input, separator, options);

    /// <inheritdoc cref="EnumerateSplitSubstrings(string, string, StringSplitOptions)"/>
    public static StringSplitByStringEnumerator EnumerateSplitSubstrings(
        this Span<char> input,
        string separator,
        StringSplitOptions options
    ) => new(input, separator, options);
    #endregion

    #region StringArray
    /// <summary>
    /// Returns an enumeration of substrings separated by specified delimiting strings and based on the given options.
    /// </summary>
    /// <param name="input">Input to enumerate.</param>
    /// <param name="separator">An array of strings that delimit the substrings in this string or an empty array that contains no delimiters.</param>
    /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim substrings and include empty substrings.</param>
    /// <returns>An enumeration of substrings that are delimited by one ore more strings in <paramref name="separator"/></returns>
    public static StringSplitByStringArrayEnumerator EnumerateSplitSubstrings(
        this string input,
        string[] separator,
        StringSplitOptions options
    ) => new(input.AsSpan(), separator, options);

    /// <inheritdoc cref="EnumerateSplitSubstrings(string, string[], StringSplitOptions)"/>
    public static StringSplitByStringArrayEnumerator EnumerateSplitSubstrings(
        this ReadOnlySpan<char> input,
        string[] separator,
        StringSplitOptions options
    ) => new(input, separator, options);

    /// <inheritdoc cref="EnumerateSplitSubstrings(string, string[], StringSplitOptions)"/>
    public static StringSplitByStringArrayEnumerator EnumerateSplitSubstrings(
        this Span<char> input,
        string[] separator,
        StringSplitOptions options
    ) => new(input, separator, options);
    #endregion
}
