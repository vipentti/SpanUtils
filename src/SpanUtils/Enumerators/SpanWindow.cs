using System;

namespace SpanUtils.Enumerators;

/// <summary>
/// A sliding window into a <see cref="Span{T}"/>
/// </summary>
public readonly ref struct SpanWindow<T>
{
    /// <summary>
    /// The current window, sliced from the original span.
    /// </summary>
    public Span<T> Span { get; }

    /// <summary>
    /// Index of the first element of this window inside the original span
    /// </summary>
    public int Index { get; }

    internal SpanWindow(Span<T> span, int index)
    {
        Span = span;
        Index = index;
    }

    public void Deconstruct(out Span<T> span, out int index)
    {
        span = Span;
        index = Index;
    }

    public static implicit operator Span<T>(SpanWindow<T> entry) => entry.Span;
    public static implicit operator ReadOnlySpan<T>(SpanWindow<T> entry) => entry.Span;
}
