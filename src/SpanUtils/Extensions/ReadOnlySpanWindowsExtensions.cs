﻿using SpanUtils.Enumerators;
using System;

namespace SpanUtils.Extensions;

/// <summary>
/// Extension methods for arrays, <see cref="ReadOnlySpan{T}"/> and <see cref="Span{T}"/>
/// </summary>
public static class ReadOnlySpanWindowsExtensions
{
    /// <summary>
    /// Enumerate read-only sliding windows inside the source
    /// </summary>
    /// <typeparam name="T">Type of values</typeparam>
    /// <param name="source">Source values to enumerate</param>
    /// <param name="size">Size of each sliding window</param>
    /// <returns>Enumerator which yields the sliding windows.</returns>
    public static ReadOnlySpanWindowsEnumerator<T> GetReadOnlyWindowsEnumerator<T>(this T[] source, int size) =>
        new(source, size);

    /// <summary>
    /// Enumerate read-only sliding windows inside the source
    /// </summary>
    /// <typeparam name="T">Type of values</typeparam>
    /// <param name="source">Source values to enumerate</param>
    /// <param name="size">Size of each sliding window</param>
    /// <returns>Enumerator which yields the sliding windows.</returns>
    public static ReadOnlySpanWindowsEnumerator<T> GetReadOnlyWindowsEnumerator<T>(this ReadOnlySpan<T> source, int size) =>
        new(source, size);

    /// <summary>
    /// Enumerate read-only sliding windows inside the source
    /// </summary>
    /// <typeparam name="T">Type of values</typeparam>
    /// <param name="source">Source values to enumerate</param>
    /// <param name="size">Size of each sliding window</param>
    /// <returns>Enumerator which yields the sliding windows.</returns>
    public static ReadOnlySpanWindowsEnumerator<T> GetReadOnlyWindowsEnumerator<T>(this Span<T> source, int size) =>
        new(source, size);
}
