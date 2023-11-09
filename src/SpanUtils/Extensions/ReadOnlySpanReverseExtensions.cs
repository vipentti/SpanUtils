﻿// Copyright 2023 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using System;
using SpanUtils.Enumerators;

namespace SpanUtils.Extensions;

/// <summary>
/// Extension methods for arrays, <see cref="ReadOnlySpan{T}"/> and <see cref="Span{T}"/>
/// </summary>
public static class ReadOnlySpanReverseExtensions
{
    /// <summary>
    /// Enumerate the contents of the source in reverse.
    /// </summary>
    /// <typeparam name="T">Type of values</typeparam>
    /// <param name="source">Source to enumerate</param>
    /// <returns>Enumerator which yields the values in reverse.</returns>
    public static ReadOnlySpanReverseEnumerator<T> GetReadOnlyReverseEnumerator<T>(
        this T[] source
    ) => new(source);

    /// <summary>
    /// Enumerate the contents of the source in reverse.
    /// </summary>
    /// <typeparam name="T">Type of values</typeparam>
    /// <param name="source">Source to enumerate</param>
    /// <returns>Enumerator which yields the values in reverse.</returns>
    public static ReadOnlySpanReverseEnumerator<T> GetReadOnlyReverseEnumerator<T>(
        this ReadOnlySpan<T> source
    ) => new(source);

    /// <summary>
    /// Enumerate the contents of the source in reverse.
    /// </summary>
    /// <typeparam name="T">Type of values</typeparam>
    /// <param name="source">Source to enumerate</param>
    /// <returns>Enumerator which yields the values in reverse.</returns>
    public static ReadOnlySpanReverseEnumerator<T> GetReadOnlyReverseEnumerator<T>(
        this Span<T> source
    ) => new(source);
}
