using SpanUtils.Enumerators;
using System;

namespace SpanUtils.Extensions;

public static class SpanReverseExtensions
{
    /// <summary>
    /// Enumerate the contents of the source in reverse.
    /// </summary>
    /// <typeparam name="T">Type of values</typeparam>
    /// <param name="source">Source to enumerate</param>
    /// <returns>Enumerator which yields the values in reverse.</returns>
    public static SpanReverseEnumerator<T> GetReverseEnumerator<T>(this T[] source) =>
        new(source);

    /// <summary>
    /// Enumerate the contents of the source in reverse.
    /// </summary>
    /// <typeparam name="T">Type of values</typeparam>
    /// <param name="source">Source to enumerate</param>
    /// <returns>Enumerator which yields the values in reverse.</returns>
    public static SpanReverseEnumerator<T> GetReverseEnumerator<T>(this Span<T> source) =>
        new(source);
}
