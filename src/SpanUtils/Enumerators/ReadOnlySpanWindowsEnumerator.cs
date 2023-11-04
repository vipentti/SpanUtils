using System;

namespace SpanUtils.Enumerators;

/// <summary>
/// Enumerate over sliding windows in a <see cref="ReadOnlySpan{T}"/>
/// </summary>
public ref struct ReadOnlySpanWindowsEnumerator<T>
{
    private int _index;
    private readonly int _windowSize;
    private readonly ReadOnlySpan<T> _span;

    /// <summary>
    /// Initialize new enumerator for the given span with the given sliding window size.
    /// </summary>
    /// <param name="span">Span to enumerate</param>
    /// <param name="windowSize">Size of the sliding window</param>
    public ReadOnlySpanWindowsEnumerator(ReadOnlySpan<T> span, int windowSize)
    {
        _index = 0;
        _windowSize = windowSize;
        _span = span;
    }

    // Needed to be compatible with the foreach operator
    public readonly ReadOnlySpanWindowsEnumerator<T> GetEnumerator() => this;

    public ReadOnlySpanWindow<T> Current { get; private set; }

    public void Reset()
    {
        _index = 0;
    }

    public bool MoveNext()
    {
        var span = _span;

        if (span.Length == 0)
            return false;

        if (_index + _windowSize <= span.Length)
        {
            var slice = span.Slice(_index, _windowSize);
            Current = new ReadOnlySpanWindow<T>(slice, _index);
            ++_index;
            return true;
        }

        return false;
    }
}
