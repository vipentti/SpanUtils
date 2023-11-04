using System;
using System.Runtime.CompilerServices;

namespace SpanUtils.Enumerators;

#if NET7_0_OR_GREATER

/// <summary>
/// Enumerate contents of a <see cref="Span{T}"/> in reverse.
/// </summary>
public ref struct SpanReverseEnumerator<T>
{
    private readonly Span<T> _span;
    private int _index;
    private ref T _current;

    /// <summary>
    /// Initialize new enumerator for the given span
    /// </summary>
    /// <param name="span">Span to enumerate</param>
    public SpanReverseEnumerator(Span<T> span)
    {
        _span = span;
        _index = _span.Length;
        _current = ref Unsafe.NullRef<T>();
    }

    // Needed to be compatible with the foreach operator
    public readonly SpanReverseEnumerator<T> GetEnumerator() => this;

    public ref T Current => ref _current;

    public void Reset()
    {
        _index = _span.Length;
    }

    public bool MoveNext()
    {
        var next = --_index;

        if (next >= 0)
        {
            _current = ref _span[next];
            _index = next;
            return true;
        }

        return false;
    }
}

#else

/// <summary>
/// Enumerate contents of a <see cref="Span{T}"/> in reverse.
/// </summary>
public ref struct SpanReverseEnumerator<T>
{
    private readonly Span<T> _span;
    private int _index;

    /// <summary>
    /// Initialize new enumerator for the given span
    /// </summary>
    /// <param name="span">Span to enumerate</param>
    public SpanReverseEnumerator(Span<T> span)
    {
        _span = span;
        _index = _span.Length;
    }

    // Needed to be compatible with the foreach operator

    public readonly SpanReverseEnumerator<T> GetEnumerator() => this;

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

#endif

