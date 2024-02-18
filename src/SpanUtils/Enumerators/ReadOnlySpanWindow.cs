// Copyright 2023-2024 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

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

    /// <summary>
    /// Index of the first element of this window inside the original span
    /// </summary>
    public int Index { get; }

    internal ReadOnlySpanWindow(ReadOnlySpan<T> span, int index)
    {
        Span = span;
        Index = index;
    }

    /// <summary>
    /// Deconstruct this
    /// </summary>
    /// <param name="span">The current span</param>
    /// <param name="index">The current index</param>
    public void Deconstruct(out ReadOnlySpan<T> span, out int index)
    {
        span = Span;
        index = Index;
    }

    /// <summary>
    /// Support implicit conversion
    /// </summary>
    public static implicit operator ReadOnlySpan<T>(ReadOnlySpanWindow<T> entry) => entry.Span;

    /// <summary>
    /// Return the contained ReadOnlySpan
    /// </summary>
    public ReadOnlySpan<T> ToReadOnlySpan() => Span;
}
