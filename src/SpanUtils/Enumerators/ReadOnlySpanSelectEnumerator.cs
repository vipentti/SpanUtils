using System;

namespace SpanUtils.Enumerators;

/// <summary>
/// Enumerate over a <see cref="ReadOnlySpan{T}"/> by mapping values with the given function
/// </summary>
/// <typeparam name="T">Input type</typeparam>
/// <typeparam name="U">Output type</typeparam>
public ref struct ReadOnlySpanSelectEnumerator<T, U>
{
    private readonly ReadOnlySpan<T> _span;
    private readonly Func<T, U> _selector;
    private int _index;

    /// <summary>
    /// Initialize new instance of the enumerator for the given span using the provided
    /// selector to map the values.
    /// </summary>
    /// <param name="span">Span to enumerate</param>
    /// <param name="selector">Function to use for mapping the values</param>
    public ReadOnlySpanSelectEnumerator(ReadOnlySpan<T> span, Func<T, U> selector)
    {
        _span = span;
        _index = -1;
        _selector = selector;
    }

#if NETSTANDARD2_0_OR_GREATER || NET6_0_OR_GREATER
    public readonly int Length => _span.Length;

    public readonly ReadOnlySpanSelectEnumerator<T, U> GetEnumerator() => this;
#else
    public int Length => _span.Length;

    public ReadOnlySpanSelectEnumerator<T, U> GetEnumerator() => this;
#endif

    public U Current { get; private set; } = default!;

    public void Reset()
    {
        _index = -1;
    }

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
