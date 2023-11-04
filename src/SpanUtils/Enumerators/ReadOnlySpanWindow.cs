using System;

namespace SpanUtils.Enumerators;

/// <summary>
/// A sliding window into a <see cref="ReadOnlySpan{T}"/>
/// </summary>
public readonly ref struct ReadOnlySpanWindow<T>
{
    /// <summary>
    /// The current window, sliced from the original span.
    /// </summary>
    public ReadOnlySpan<T> Span { get; }

    public int Index { get; }

    internal ReadOnlySpanWindow(ReadOnlySpan<T> span, int index)
    {
        Span = span;
        Index = index;
    }

    public void Deconstruct(out ReadOnlySpan<T> span, out int index)
    {
        span = Span;
        index = Index;
    }

    public static implicit operator ReadOnlySpan<T>(ReadOnlySpanWindow<T> entry) => entry.Span;
}
