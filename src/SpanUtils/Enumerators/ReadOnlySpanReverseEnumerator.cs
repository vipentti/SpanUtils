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
    }

    // Needed to be compatible with the foreach operator
    public readonly ReadOnlySpanReverseEnumerator<T> GetEnumerator() => this;

    public T Current { get; private set; } = default!;

    public void Reset()
    {
        _index = _span.Length;
    }

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
