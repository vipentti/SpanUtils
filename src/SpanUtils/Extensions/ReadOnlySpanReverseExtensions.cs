using SpanUtils.Enumerators;
using System;

namespace SpanUtils.Extensions;

public static class ReadOnlySpanReverseExtensions
{
    /// <summary>
    /// Enumerate the contents of the source in reverse.
    /// </summary>
    /// <typeparam name="T">Type of values</typeparam>
    /// <param name="source">Source to enumerate</param>
    /// <returns>Enumerator which yields the values in reverse.</returns>
    public static ReadOnlySpanReverseEnumerator<T> GetReadOnlyReverseEnumerator<T>(this T[] source) =>
        new(source);

    /// <summary>
    /// Enumerate the contents of the source in reverse.
    /// </summary>
    /// <typeparam name="T">Type of values</typeparam>
    /// <param name="source">Source to enumerate</param>
    /// <returns>Enumerator which yields the values in reverse.</returns>
    public static ReadOnlySpanReverseEnumerator<T> GetReadOnlyReverseEnumerator<T>(this ReadOnlySpan<T> source) =>
        new(source);

    /// <summary>
    /// Enumerate the contents of the source in reverse.
    /// </summary>
    /// <typeparam name="T">Type of values</typeparam>
    /// <param name="source">Source to enumerate</param>
    /// <returns>Enumerator which yields the values in reverse.</returns>
    public static ReadOnlySpanReverseEnumerator<T> GetReadOnlyReverseEnumerator<T>(this Span<T> source) =>
        new(source);
}
