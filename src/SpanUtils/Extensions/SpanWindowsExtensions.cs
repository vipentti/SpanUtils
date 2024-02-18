// Copyright 2023-2024 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using System;
using SpanUtils.Enumerators;

namespace SpanUtils.Extensions;

/// <summary>
/// Extension methods for arrays, <see cref="ReadOnlySpan{T}"/> and <see cref="Span{T}"/>
/// </summary>
public static class SpanWindowsExtensions
{
    /// <summary>
    /// Enumerate sliding windows inside the source
    /// </summary>
    /// <typeparam name="T">Type of values</typeparam>
    /// <param name="source">Source values to enumerate</param>
    /// <param name="size">Size of each sliding window</param>
    /// <returns>Enumerator which yields the sliding windows.</returns>
    public static SpanWindowsEnumerator<T> EnumerateWindows<T>(this Span<T> source, int size) =>
        new(source, size);

    /// <inheritdoc cref="EnumerateWindows{T}(Span{T}, int)"/>
    public static SpanWindowsEnumerator<T> EnumerateWindows<T>(this T[] source, int size) =>
        new(source, size);
}
