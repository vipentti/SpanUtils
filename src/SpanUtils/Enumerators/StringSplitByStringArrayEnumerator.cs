// Copyright 2023-2024 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using System;
using System.Linq;

namespace SpanUtils.Enumerators;

/// <summary>
/// Enumerates substrings of a <see cref="ReadOnlySpan{Char}"/> separated by specified delimiting characters and options.
/// </summary>
/// <remarks>
/// To get an instance of this type, use <see cref="Extensions.StringExtensions.EnumerateSplitSubstrings(ReadOnlySpan{char}, string[], StringSplitOptions)"/>.
/// </remarks>
public ref struct StringSplitByStringArrayEnumerator
{
    private readonly string[] _sep;
    private readonly bool _onlyWhitespace;
    private readonly StringSplitOptions _options;

    private ReadOnlySpan<char> _remaining;
    private bool _yieldEmpty;
    private bool _returnOnce;

    internal StringSplitByStringArrayEnumerator(
        ReadOnlySpan<char> str,
        string[] sep,
        StringSplitOptions options
    )
    {
        _options = options;
        _remaining = str;
        _yieldEmpty = false;
        _returnOnce = false;
        _onlyWhitespace = false;
        Current = default;

        if (sep is null || sep.Length == 0)
        {
            _sep = StringSplitUtils.EmptyStringArray;
            _onlyWhitespace = true;
        }
        else
        {
            if (sep.All(string.IsNullOrEmpty))
            {
                _returnOnce = true;
                _sep = StringSplitUtils.EmptyStringArray;
            }
            else
            {
                _sep = sep;
            }
        }
    }

    /// <summary>
    /// Gets the current substring
    /// </summary>
    public ReadOnlySpan<char> Current { get; private set; }

    /// <summary>
    /// Returns this instance as an enumerator.
    /// </summary>
    public readonly StringSplitByStringArrayEnumerator GetEnumerator() => this;

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
        var options = _options;
        var yieldEmpty = _yieldEmpty;

        while (
            StringSplitUtils.TryMoveNext(
                ref span,
                ref yieldEmpty,
                GetIndex(in span, out var sepLen),
                sepLen,
                options,
                out var next
            )
        )
        {
            if (_options.HasFlag(StringSplitOptions.RemoveEmptyEntries) && next.IsEmpty)
            {
                continue;
            }

            Current = next;
            _remaining = span;
            _yieldEmpty = yieldEmpty;
            return true;
        }

        _yieldEmpty = yieldEmpty;
        _remaining = span;
        return false;
    }

    private readonly int GetIndex(in ReadOnlySpan<char> span, out int separatorLength)
    {
        if (_onlyWhitespace)
        {
            separatorLength = 1;
            return span.IndexOfAny(StringSplitUtils.WhitespaceChars);
        }

        // TODO: Can this be optimized?
        var found = false;
        var minIndex = int.MaxValue;
        separatorLength = 0;

        var searchWindow = span;

        foreach (var sep in _sep)
        {
            // will not find the match
            if (sep.Length > searchWindow.Length)
            {
                continue;
            }

            var current = searchWindow.IndexOf(sep.AsSpan());

            if (current > -1 && current < minIndex)
            {
                found = true;
                minIndex = current;
                separatorLength = sep.Length;
            }
        }

        if (found)
        {
            return minIndex;
        }

        return -1;
    }
}
