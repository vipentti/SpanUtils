// Copyright 2023-2024 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using System;
using SpanUtils.Enumerators;

namespace SpanUtils.Extensions;

/// <summary>
/// Extension methods for arrays, <see cref="ReadOnlySpan{T}"/> and <see cref="Span{T}"/>
/// </summary>
public static class ReadOnlySpanSelectExtensions
{
    /// <summary>
    /// Enumerate over the source by mapping values with the given function
    /// </summary>
    /// <typeparam name="TInput">Input type</typeparam>
    /// <typeparam name="TOutput">Output type</typeparam>
    /// <returns>Enumerator which maps the values contained in the source with the given selector</returns>
    public static ReadOnlySpanSelectEnumerator<TInput, TOutput> EnumerateReadOnlySelect<
        TInput,
        TOutput
    >(this ReadOnlySpan<TInput> source, Func<TInput, TOutput> selector) => new(source, selector);

    /// <inheritdoc cref="EnumerateReadOnlySelect{TInput, TOutput}(ReadOnlySpan{TInput}, Func{TInput, TOutput})"/>
    public static ReadOnlySpanSelectEnumerator<TInput, TOutput> EnumerateReadOnlySelect<
        TInput,
        TOutput
    >(this TInput[] source, Func<TInput, TOutput> selector) => new(source, selector);

    /// <inheritdoc cref="EnumerateReadOnlySelect{TInput, TOutput}(ReadOnlySpan{TInput}, Func{TInput, TOutput})"/>
    public static ReadOnlySpanSelectEnumerator<TInput, TOutput> EnumerateReadOnlySelect<
        TInput,
        TOutput
    >(this Span<TInput> source, Func<TInput, TOutput> selector) => new(source, selector);
}
