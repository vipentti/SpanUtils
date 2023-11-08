using System;

namespace SpanUtils.Enumerators;

/// <summary>
/// Enumerates substrings of a <see cref="ReadOnlySpan{Char}"/> separated by specified delimiting character and options.
/// </summary>
/// <remarks>
/// To get an instance of this type, use <see cref="Extensions.StringExtensions.EnumerateSplitSubstrings(ReadOnlySpan{char}, char, StringSplitOptions)"/>.
/// </remarks>
public ref struct StringSplitByCharEnumerator
{
    private readonly char _sep;
    private readonly StringSplitOptions _options;

    private ReadOnlySpan<char> _remaining;
    private bool _yieldEmpty;

    internal StringSplitByCharEnumerator(
        ReadOnlySpan<char> str,
        char sep,
        StringSplitOptions options)
    {
        _sep = sep;
        _options = options;
        _remaining = str;
        _yieldEmpty = false;
    }

    /// <summary>
    /// Gets the current substring
    /// </summary>
    public ReadOnlySpan<char> Current { get; private set; }

    /// <summary>
    /// Returns this instance as an enumerator.
    /// </summary>
    public readonly StringSplitByCharEnumerator GetEnumerator() => this;

    /// <summary>
    /// Advances the enumerator to the next substring in the span.
    /// </summary>
    /// <returns>
    /// True if the enumerator successfully advanced to the next substring; false if
    /// the enumerator has advanced past the end of the span.
    /// </returns>
    public bool MoveNext()
    {
        var span = _remaining;
        var sep = _sep;
        var options = _options;
        var empty = _yieldEmpty;

        while (StringSplitUtils.TryMoveNext(
            ref span,
            ref empty,
            span.IndexOf(sep),
            1,
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
