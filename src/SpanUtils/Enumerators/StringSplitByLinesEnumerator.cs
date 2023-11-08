using System;

namespace SpanUtils.Enumerators;

/// <summary>
/// Enumerates the lines of a <see cref="ReadOnlySpan{Char}"/>.
/// </summary>
/// <remarks>
/// To get an instance of this type, use <see cref="Extensions.StringExtensions.EnumerateSplitLines(ReadOnlySpan{char})"/>.
/// </remarks>
public ref struct StringSplitByLinesEnumerator
{
    private ReadOnlySpan<char> _remaining;
    private bool _isDone;

    internal StringSplitByLinesEnumerator(ReadOnlySpan<char> str)
    {
        _remaining = str;
        _isDone = false;
        Current = default;
    }

    /// <summary>
    /// Gets the current line in the span.
    /// </summary>
    public ReadOnlySpan<char> Current { get; private set; }

    /// <summary>
    /// Returns this instance as an enumerator.
    /// </summary>
    public readonly StringSplitByLinesEnumerator GetEnumerator() => this;

    /// <summary>
    /// Advances the enumerator to the next line in the span.
    /// </summary>
    /// <returns>
    /// True if the enumerator successfully advanced to the next line; false if
    /// the enumerator has advanced past the end of the span.
    /// </returns>
    public bool MoveNext()
    {
        if (_isDone)
        {
            return false;
        }

        var span = _remaining;
        var len = span.Length;

        var index = span.IndexOfAny(StringSplitUtils.NewlineChars);

        if (index == -1)
        {
            // EOF
            Current = span;
            _remaining = default;
            _isDone = true;
        }
        else
        {
            var step = 1;

            // Check if we have CR + LF
            if (span[index] == '\r' && index + 1 < len && span[index + 1] == '\n')
            {
                step = 2;
            }

#pragma warning disable IDE0057 // Use range operator
            Current = span.Slice(0, index);
            _remaining = span.Slice(index + step);
#pragma warning restore IDE0057 // Use range operator
        }

        return true;
    }
}
