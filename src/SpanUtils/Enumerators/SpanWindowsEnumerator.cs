// Copyright 2023 Ville Penttinen
// Distributed under the MIT License.
// https://github.com/vipentti/SpanUtils/blob/main/LICENSE.md

using System;

namespace SpanUtils.Enumerators;

/// <summary>
/// Enumerate over sliding windows in a <see cref="Span{T}"/>
/// </summary>
public ref struct SpanWindowsEnumerator<T>
{
    private int _index;
    private readonly int _windowSize;
    private readonly Span<T> _span;

    /// <summary>
    /// Initialize new enumerator for the given span with the given sliding window size.
    /// </summary>
    /// <param name="span">Span to enumerate</param>
    /// <param name="windowSize">Size of the sliding window</param>
    public SpanWindowsEnumerator(Span<T> span, int windowSize)
    {
        _windowSize = windowSize;
        _span = span;
        _index = 0;
    }

    /// <summary>
    /// Returns this instance as an enumerator.
    /// </summary>
    public readonly SpanWindowsEnumerator<T> GetEnumerator() => this;

    /// <summary>
    /// Gets the current window
    /// </summary>
    public SpanWindow<T> Current { get; private set; }

    /// <summary>
    /// Resets this enumerator
    /// </summary>
    public void Reset()
    {
        _index = 0;
    }

    /// <summary>
    /// Advances the enumerator to the next window in the span.
    /// </summary>
    /// <returns>
    /// True if the enumerator successfully advanced to the next window; false if
    /// the enumerator has advanced past the end of the span.
    /// </returns>
    public bool MoveNext()
    {
        var span = _span;

        if (span.Length == 0)
            return false;

        if (_index + _windowSize <= span.Length)
        {
            var slice = span.Slice(_index, _windowSize);

            Current = new SpanWindow<T>(slice, _index);

            ++_index;

            return true;
        }

        return false;
    }
}
