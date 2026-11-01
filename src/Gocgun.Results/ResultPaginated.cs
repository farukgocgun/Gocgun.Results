namespace Gocgun.Results;

/// <summary>
/// One page of rows. <see cref="Item"/> is empty when the result failed.
/// The client tracks paging itself: it sends the last row's key as the next cursor and stops when a page is shorter than its limit.
/// </summary>
/// <typeparam name="T">Row type.</typeparam>
public sealed class ResultPaginated<T> : Result, IResultFactory<ResultPaginated<T>>
{
    /// <summary>Initializes a new instance of the <see cref="ResultPaginated{T}"/> class.</summary>
    private ResultPaginated(IReadOnlyList<T> item, int? total, Error? error)
        : base(error)
    {
        Item = item;
        Total = total;
    }

    /// <summary>Gets the page. Empty when the result failed.</summary>
    public IReadOnlyList<T> Item { get; }

    /// <summary>Gets the total row count when the caller computed it.</summary>
    public int? Total { get; }

    /// <summary>Creates a successful page.</summary>
    /// <param name="item">Rows on this page.</param>
    /// <param name="total">Total rows when known.</param>
    /// <returns>A successful page.</returns>
    public static ResultPaginated<T> Success(IReadOnlyList<T> item, int? total = null)
    {
        ArgumentNullException.ThrowIfNull(item);
        return new(item, total, null);
    }

    /// <summary>Creates a failed page with an empty item list.</summary>
    /// <param name="failure">The failure.</param>
    /// <returns>A failed page.</returns>
    public static new ResultPaginated<T> Failure(Error failure)
    {
        ArgumentNullException.ThrowIfNull(failure);
        return new([], null, failure);
    }
}
