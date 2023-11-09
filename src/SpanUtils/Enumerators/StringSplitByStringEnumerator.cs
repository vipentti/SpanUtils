// Copyright 2023 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using System;

namespace SpanUtils.Enumerators;

/// <summary>
/// Enumerates substrings of a <see cref="ReadOnlySpan{Char}"/> separated by specified delimiting characters and options.
/// </summary>
/// <remarks>
/// To get an instance of this type, use <see cref="Extensions.StringExtensions.EnumerateSplitSubstrings(ReadOnlySpan{char}, string, StringSplitOptions)"/>.
/// </remarks>
public ref struct StringSplitByStringEnumerator
{
    private readonly ReadOnlySpan<char> _sep;
    private readonly StringSplitOptions _options;

    private ReadOnlySpan<char> _remaining;
    private bool _yieldEmpty;
    private bool _returnOnce;

    internal StringSplitByStringEnumerator(
        ReadOnlySpan<char> str,
        string sep,
        StringSplitOptions options)
    {
        _options = options;
        _remaining = str;
        _yieldEmpty = false;
        Current = default;

        // Empty separator or longer separator than string -> return only the original string
        if (string.IsNullOrEmpty(sep) || sep.Length > str.Length)
        {
            _sep = default;
            _returnOnce = true;
        }
        else
        {
            _sep = sep.AsSpan();
            _returnOnce = false;
        }
    }

    /// <summary>
    /// Gets the current substring
    /// </summary>
    public ReadOnlySpan<char> Current { get; private set; }

    /// <summary>
    /// Returns this instance as an enumerator.
    /// </summary>
    public readonly StringSplitByStringEnumerator GetEnumerator() => this;

    /// <summary>
    /// Advances the enumerator to the next substring in the span.
    /// </summary>
    /// <returns>
    /// True if the enumerator successfully advanced to the next substring; false if
    /// the enumerator has advanced past the end of the span.
    /// </returns>
    public bool MoveNext()
    {
        if (_returnOnce)
        {
            Current = _remaining;
            _returnOnce = false;
            _remaining = default;
            return true;
        }

        var span = _remaining;
        var sep = _sep;
        var len = _sep.Length;
        var options = _options;
        var empty = _yieldEmpty;

        while (StringSplitUtils.TryMoveNext(
            ref span,
            ref empty,
            span.IndexOf(sep),
            len,
            options,
            out var next))
        {
            if (_options.HasFlag(StringSplitOptions.RemoveEmptyEntries) && next.IsEmpty)
            {
                continue;
            }

            Current = next;
            _remaining = span;
            _yieldEmpty = empty;
            return true;
        }

        _yieldEmpty = empty;
        _remaining = span;
        return false;
    }
}
