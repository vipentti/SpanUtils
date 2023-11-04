using System;

namespace SpanUtils.Enumerators;

/// <summary>
/// Contiguous chunk inside a <see cref="Span{T}"/> enumerated with <see cref="SpanChunksEnumerator{T}"/>
/// </summary>
public readonly ref struct SpanChunk<T>
{
    internal SpanChunk(Span<T> span, int index)
    {
        Span = span;
        Index = index;
    }

    /// <summary>
    /// Points to a contiguous chunk inside the original span.
    /// </summary>
    public Span<T> Span { get; }

    /// <summary>
    /// Index of the first element of this chunk inside the original span
    /// </summary>
    public int Index { get; }

    public void Deconstruct(out Span<T> span, out int index)
    {
        span = Span;
        index = Index;
    }

    public static implicit operator Span<T>(SpanChunk<T> entry) => entry.Span;
    public static implicit operator ReadOnlySpan<T>(SpanChunk<T> entry) => entry.Span;
}
