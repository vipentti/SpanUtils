using SpanUtils.Enumerators;
using System;

namespace SpanUtils.Extensions;

/// <summary>
/// Extension methods for arrays, <see cref="ReadOnlySpan{T}"/> and <see cref="Span{T}"/>
/// </summary>
public static class ReadOnlySpanSelectExtensions
{
    /// <summary>
    /// Enumerate over the source by mapping values with the given function
    /// </summary>
    /// <typeparam name="T">Input type</typeparam>
    /// <typeparam name="U">Output type</typeparam>
    /// <param name="source">Source to enumerate</param>
    /// <param name="selector">Function to map the input values with</param>
    /// <returns>Enumerator which maps the values contained in the source with the given selector</returns>
    public static ReadOnlySpanSelectEnumerator<T, U> GetReadOnlySelectEnumerator<T, U>(this T[] source, Func<T, U> selector) =>
        new(source, selector);

    /// <summary>
    /// Enumerate over the source by mapping values with the given function
    /// </summary>
    /// <typeparam name="T">Input type</typeparam>
    /// <typeparam name="U">Output type</typeparam>
    /// <returns>Enumerator which maps the values contained in the source with the given selector</returns>
    public static ReadOnlySpanSelectEnumerator<T, U> GetReadOnlySelectEnumerator<T, U>(this ReadOnlySpan<T> source, Func<T, U> selector) =>
        new(source, selector);

    /// <summary>
    /// Enumerate over the source by mapping values with the given function
    /// </summary>
    /// <typeparam name="T">Input type</typeparam>
    /// <typeparam name="U">Output type</typeparam>
    /// <returns>Enumerator which maps the values contained in the source with the given selector</returns>
    public static ReadOnlySpanSelectEnumerator<T, U> GetReadOnlySelectEnumerator<T, U>(this Span<T> source, Func<T, U> selector) =>
        new(source, selector);
}
