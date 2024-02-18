// Copyright 2023 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using System;

namespace SpanUtils.Enumerators;

/// <summary>
/// Enumerate over a <see cref="ReadOnlySpan{T}"/> by mapping values with the given function
/// </summary>
/// <typeparam name="TInput">Input type</typeparam>
/// <typeparam name="TOutput">Output type</typeparam>
public ref struct ReadOnlySpanSelectEnumerator<TInput, TOutput>
{
    private readonly ReadOnlySpan<TInput> _span;
    private readonly Func<TInput, TOutput> _selector;
    private int _index;

    /// <summary>
    /// Initialize new instance of the enumerator for the given span using the provided
    /// selector to map the values.
    /// </summary>
    /// <param name="span">Span to enumerate</param>
    /// <param name="selector">Function to use for mapping the values</param>
    public ReadOnlySpanSelectEnumerator(ReadOnlySpan<TInput> span, Func<TInput, TOutput> selector)
    {
        _span = span;
        _index = -1;
        _selector = selector;
        Current = default!;
    }

    /// <summary>
    /// Returns the length of the original span
    /// </summary>
    public readonly int Length => _span.Length;

    /// <summary>
    /// Returns this instance as an enumerator.
    /// </summary>
    public readonly ReadOnlySpanSelectEnumerator<TInput, TOutput> GetEnumerator() => this;

    /// <summary>
    /// Gets the current value
    /// </summary>
    public TOutput Current { get; private set; }

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

        if (next < _span.Length)
        {
            Current = _selector(_span[next]);
            _index = next;
            return true;
        }

        return false;
    }
}
