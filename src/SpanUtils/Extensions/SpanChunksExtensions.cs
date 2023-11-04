﻿using SpanUtils.Enumerators;
using System;

namespace SpanUtils.Extensions;

public static class SpanChunksExtensions
{
    /// <summary>
    /// Enumerate chunks inside the given source
    /// </summary>
    /// <typeparam name="T">Type of values</typeparam>
    /// <param name="source">Source values to enumerate</param>
    /// <param name="size">Size of each chunk</param>
    /// <returns>Enumerator which yields the chunks.</returns>
    public static SpanChunksEnumerator<T> GetChunksEnumerator<T>(this T[] source, int size) =>
        new(source, size);

    /// <summary>
    /// Enumerate chunks inside the given source
    /// </summary>
    /// <typeparam name="T">Type of values</typeparam>
    /// <param name="source">Source values to enumerate</param>
    /// <param name="size">Size of each chunk</param>
    /// <param name="exact">Whether only chunks matching the exact size will be enumerated. Default is <c>true</c></param>
    /// <returns>Enumerator which yields the chunks.</returns>
    public static SpanChunksEnumerator<T> GetChunksEnumerator<T>(this T[] source, int size, bool exact) =>
        new(source, size, exact);

    /// <summary>
    /// Enumerate chunks inside the given source
    /// </summary>
    /// <typeparam name="T">Type of values</typeparam>
    /// <param name="source">Source values to enumerate</param>
    /// <param name="size">Size of each chunk</param>
    /// <returns>Enumerator which yields the chunks.</returns>
    public static SpanChunksEnumerator<T> GetChunksEnumerator<T>(this Span<T> source, int size) =>
        new(source, size);

    /// <summary>
    /// Enumerate chunks inside the given source
    /// </summary>
    /// <typeparam name="T">Type of values</typeparam>
    /// <param name="source">Source values to enumerate</param>
    /// <param name="size">Size of each chunk</param>
    /// <param name="exact">Whether only chunks matching the exact size will be enumerated. Default is <c>true</c></param>
    /// <returns>Enumerator which yields the chunks.</returns>
    public static SpanChunksEnumerator<T> GetChunksEnumerator<T>(this Span<T> source, int size, bool exact) =>
        new(source, size, exact);
}
