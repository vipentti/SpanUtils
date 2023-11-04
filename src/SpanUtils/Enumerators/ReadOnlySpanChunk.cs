using System;

namespace SpanUtils.Enumerators;

/// <summary>
/// Contiguous chunk inside a <see cref="ReadOnlySpan{T}"/> enumerated with <see cref="ReadOnlySpanChunksEnumerator{T}"/>
/// </summary>
public readonly ref struct ReadOnlySpanChunk<T>
{
    internal ReadOnlySpanChunk(ReadOnlySpan<T> span, int index)
    {
        Span = span;
        Index = index;
    }

    /// <summary>
    /// Points to a contiguous chunk inside the original span.
    /// </summary>
    public ReadOnlySpan<T> Span { get; }

    /// <summary>
    /// Index of the first element of this chunk inside the original span
    /// </summary>
    public int Index { get; }

    public void Deconstruct(out ReadOnlySpan<T> span, out int index)
    {
        span = Span;
        index = Index;
    }

    public static implicit operator ReadOnlySpan<T>(ReadOnlySpanChunk<T> entry) => entry.Span;
}
