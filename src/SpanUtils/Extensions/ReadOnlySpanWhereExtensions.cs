// Copyright 2023-2024 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using System;
using SpanUtils.Enumerators;

namespace SpanUtils.Extensions;

/// <summary>
/// Extension methods for arrays, <see cref="ReadOnlySpan{T}"/> and <see cref="Span{T}"/>
/// </summary>
public static class ReadOnlySpanWhereExtensions
{
    /// <summary>
    /// Enumerate the contents of the source yielding values which match the given predicate.
    /// </summary>
    /// <typeparam name="T">Type of values</typeparam>
    /// <param name="source">Source to enumerate</param>
    /// <param name="predicate">Predicate used for selecting values to yield</param>
    /// <returns>Enumerator which yields values which match the given predicate</returns>
    public static ReadOnlySpanWhereEnumerator<T> EnumerateReadOnlyWhere<T>(
        this ReadOnlySpan<T> source,
        Predicate<T> predicate
    ) => new(source, predicate);

    /// <inheritdoc cref="EnumerateReadOnlyWhere{T}(ReadOnlySpan{T}, Predicate{T})"/>
    public static ReadOnlySpanWhereEnumerator<T> EnumerateReadOnlyWhere<T>(
        this T[] source,
        Predicate<T> predicate
    ) => new(source, predicate);

    /// <inheritdoc cref="EnumerateReadOnlyWhere{T}(ReadOnlySpan{T}, Predicate{T})"/>
    public static ReadOnlySpanWhereEnumerator<T> EnumerateReadOnlyWhere<T>(
        this Span<T> source,
        Predicate<T> predicate
    ) => new(source, predicate);
}
