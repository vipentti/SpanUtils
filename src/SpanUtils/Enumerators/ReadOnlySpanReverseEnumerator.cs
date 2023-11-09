// Copyright 2023 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using System;

namespace SpanUtils.Enumerators;

/// <summary>
/// Enumerate contents of a <see cref="ReadOnlySpan{T}"/> in reverse.
/// </summary>
public ref struct ReadOnlySpanReverseEnumerator<T>
{
    private readonly ReadOnlySpan<T> _span;
    private int _index;

    /// <summary>
    /// Initialize new enumerator for the given span
    /// </summary>
    /// <param name="span">Span to enumerate</param>
    public ReadOnlySpanReverseEnumerator(ReadOnlySpan<T> span)
    {
        _span = span;
        _index = _span.Length;
        Current = default!;
    }

    /// <summary>
    /// Returns the length of the original span
    /// </summary>
    public readonly int Length => _span.Length;

    /// <summary>
    /// Returns this instance as an enumerator.
    /// </summary>
    public readonly ReadOnlySpanReverseEnumerator<T> GetEnumerator() => this;

    /// <summary>
    /// Gets the current value
    /// </summary>
    public T Current { get; private set; }

    /// <summary>
    /// Resets this enumerator
    /// </summary>
    public void Reset()
    {
        _index = _span.Length;
    }

    /// <summary>
    /// Advances the enumerator to the next element in the span.
    /// </summary>
    /// <returns>
    /// True if the enumerator successfully advanced to the next element; false if
    /// the enumerator has advanced past the end of the span.
    /// </returns>
    public bool MoveNext()
    {
        var next = --_index;

        if (next >= 0)
        {
            Current = _span[next];
            _index = next;
            return true;
        }

        return false;
    }
}
