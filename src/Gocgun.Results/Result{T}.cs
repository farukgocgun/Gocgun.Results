namespace Gocgun.Results;

/// <summary>
/// Success or failure with a payload named <see cref="Item"/>.
/// </summary>
/// <typeparam name="T">Payload type.</typeparam>
public sealed class Result<T> : Result, IResultFactory<Result<T>>
{
    /// <summary>Initializes a new instance of the <see cref="Result{T}"/> class.</summary>
    /// <param name="item">Payload. Default when the result failed.</param>
    /// <param name="error">Failure, or null for success.</param>
    internal Result(T? item, Error? error)
        : base(error)
    {
        Item = item;
    }

    /// <summary>Gets the payload. Default when <see cref="Result.IsFailure"/> is true.</summary>
    public T? Item { get; }

    /// <summary>Converts a value into a successful result.</summary>
    /// <param name="item">Payload.</param>
    public static implicit operator Result<T>(T item) => FromT(item);

    /// <summary>Converts an error into a failed result.</summary>
    /// <param name="failure">The failure.</param>
    public static implicit operator Result<T>(Error failure) => FromError(failure);

    /// <summary>Creates a successful result from <paramref name="item"/>.</summary>
    /// <param name="item">Payload.</param>
    /// <returns>A successful result.</returns>
    public static Result<T> FromT(T item) => Success(item);

    /// <summary>Creates a failed result from <paramref name="failure"/>.</summary>
    /// <param name="failure">The failure.</param>
    /// <returns>A failed result.</returns>
    public static new Result<T> FromError(Error failure) => Failure(failure);

    /// <summary>Creates a failed result with a default payload.</summary>
    /// <param name="failure">The failure.</param>
    /// <returns>A failed result.</returns>
    public static new Result<T> Failure(Error failure)
    {
        ArgumentNullException.ThrowIfNull(failure);
        return new(default, failure);
    }
}
