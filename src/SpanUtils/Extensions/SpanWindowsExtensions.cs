using SpanUtils.Enumerators;
using System;

namespace SpanUtils.Extensions;

public static class SpanWindowsExtensions
{
    /// <summary>
    /// Enumerate sliding windows inside the source
    /// </summary>
    /// <typeparam name="T">Type of values</typeparam>
    /// <param name="source">Source values to enumerate</param>
    /// <param name="size">Size of each sliding window</param>
    /// <returns>Enumerator which yields the sliding windows.</returns>
    public static SpanWindowsEnumerator<T> GetWindowsEnumerator<T>(this T[] source, int size) =>
        new(source, size);

    /// <summary>
    /// Enumerate sliding windows inside the source
    /// </summary>
    /// <typeparam name="T">Type of values</typeparam>
    /// <param name="source">Source values to enumerate</param>
    /// <param name="size">Size of each sliding window</param>
    /// <returns>Enumerator which yields the sliding windows.</returns>
    public static SpanWindowsEnumerator<T> GetWindowsEnumerator<T>(this Span<T> source, int size) =>
        new(source, size);
}
