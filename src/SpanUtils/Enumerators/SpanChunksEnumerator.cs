// Copyright 2023-2024 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using System;

namespace SpanUtils.Enumerators;

/// <summary>
/// Enumerator over sized chunks in a <see cref="Span{T}"/>
/// </summary>
public ref struct SpanChunksEnumerator<T>
{
    private int _index;
    private readonly bool _exact;
    private readonly int _chunkSize;
    private readonly Span<T> _span;

    /// <summary>
    /// Initialize new enumerator for the given span with the given chunk size.
    /// When exact is true, only chunks which match the chunk size exactly will be enumerated.
    /// </summary>
    /// <param name="span">Span to enumerate over</param>
    /// <param name="chunkSize">Size of chunks to return</param>
    /// <param name="exact">Whether only chunks matching the exact size will be enumerated</param>
    /// <exception cref="ArgumentException">When <paramref name="chunkSize"/> is 0 or negative</exception>
    public SpanChunksEnumerator(Span<T> span, int chunkSize, bool exact = true)
    {
        if (chunkSize <= 0)
        {
            throw new ArgumentException("Chunk size must be greater than 0", nameof(chunkSize));
        }

        _index = 0;
        _chunkSize = chunkSize;
        _exact = exact;
        _span = span;
    }

    /// <summary>
    /// Returns this instance as an enumerator.
    /// </summary>
    public readonly SpanChunksEnumerator<T> GetEnumerator() => this;

    /// <summary>
    /// Gets the current chunk
    /// </summary>
    public SpanChunk<T> Current { get; private set; }

    /// <summary>
    /// Resets this enumerator
    /// </summary>
    public void Reset()
    {
        _index = 0;
    }

    /// <summary>
    /// Advances the enumerator to the next chunk in the span.
    /// </summary>
    /// <returns>
    /// True if the enumerator successfully advanced to the next chunk; false if
    /// the enumerator has advanced past the end of the span.
    /// </returns>
    public bool MoveNext()
    {
        var span = _span;

        if (span.Length == 0)
            return false;

        var index = _index;
        var chunkSize = _chunkSize;

        // Full chunks
        if (index + chunkSize <= span.Length)
        {
            var slice = span.Slice(index, chunkSize);
            Current = new SpanChunk<T>(slice, index);
            _index += chunkSize;
            return true;
        }

        // remainder
        if (!_exact && index < span.Length)
        {
            var slice = span.Slice(index);
            Current = new SpanChunk<T>(slice, index);
            _index += slice.Length;
            return true;
        }

        return false;
    }
}
