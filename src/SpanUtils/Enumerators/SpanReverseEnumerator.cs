using System;

namespace SpanUtils.Enumerators;

#if NET7_0_OR_GREATER
using System.Runtime.CompilerServices;

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

    /// <summary>
    /// Returns this instance as an enumerator.
    /// </summary>
    public readonly SpanReverseEnumerator<T> GetEnumerator() => this;

    /// <summary>
    /// Gets the current value
    /// </summary>
    public ref T Current => ref _current;

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
        Current = default!;
    }

    /// <summary>
    /// Returns this instance as an enumerator.
    /// </summary>
    public readonly SpanReverseEnumerator<T> GetEnumerator() => this;

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

#endif

