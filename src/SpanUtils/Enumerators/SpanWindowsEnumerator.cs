using System;

namespace SpanUtils.Enumerators;

/// <summary>
/// Enumerate over sliding windows in a <see cref="Span{T}"/>
/// </summary>
public ref struct SpanWindowsEnumerator<T>
{
    private int _index;
    private readonly int _windowSize;
    private readonly Span<T> _span;

    /// <summary>
    /// Initialize new enumerator for the given span with the given sliding window size.
    /// </summary>
    /// <param name="span">Span to enumerate</param>
    /// <param name="windowSize">Size of the sliding window</param>
    public SpanWindowsEnumerator(Span<T> span, int windowSize)
    {
        _windowSize = windowSize;
        _span = span;
        _index = 0;
    }

    // Needed to be compatible with the foreach operator
    public readonly SpanWindowsEnumerator<T> GetEnumerator() => this;

    public SpanWindow<T> Current { get; private set; }

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

            Current = new SpanWindow<T>(slice, _index);

            ++_index;

            return true;
        }

        return false;
    }
}
