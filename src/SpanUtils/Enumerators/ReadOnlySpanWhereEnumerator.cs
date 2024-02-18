// Copyright 2023-2024 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using System;

namespace SpanUtils.Enumerators;

/// <summary>
/// Enumerate over a <see cref="ReadOnlySpan{T}"/> by yielding values which match the given predicate.
/// </summary>
/// <typeparam name="T">Type of element in the span</typeparam>
public ref struct ReadOnlySpanWhereEnumerator<T>
{
    private readonly ReadOnlySpan<T> _span;
    private readonly Predicate<T> _predicate;
    private int _index;

    /// <summary>
    /// Initialize new instance of the enumerator for the given span using the provided
    /// predicate to filter values to yield.
    /// </summary>
    /// <param name="span">Span to enumerate</param>
    /// <param name="predicate">Predicate used for selecting values to yield</param>
    public ReadOnlySpanWhereEnumerator(ReadOnlySpan<T> span, Predicate<T> predicate)
    {
        _span = span;
        _index = -1;
        _predicate = predicate;
        Current = default!;
    }

    /// <summary>
    /// Returns the length of the original span
    /// </summary>
    public readonly int Length => _span.Length;

    /// <summary>
    /// Returns this instance as an enumerator.
    /// </summary>
    public readonly ReadOnlySpanWhereEnumerator<T> GetEnumerator() => this;

    /// <summary>
    /// Gets the current value
    /// </summary>
    public T Current { get; private set; }

    /// <summary>
    /// Resets this enumerator
    /// </summary>
    public void Reset()
    {
        _index = -1;
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
        var next = _index + 1;
        var len = _span.Length;

        while (next < len)
        {
            if (_predicate(_span[next]))
            {
                Current = _span[next];
                _index = next;
                return true;
            }

            ++next;
        }

        _index = next;
        return false;
    }
}
