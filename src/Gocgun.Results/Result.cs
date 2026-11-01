namespace Gocgun.Results;

/// <summary>
/// Success or failure without a payload. Construct only through the factories.
/// </summary>
public class Result : IResultFactory<Result>
{
    /// <summary>Initializes a new instance of the <see cref="Result"/> class.</summary>
    /// <param name="error">Failure, or null for success.</param>
    protected Result(Error? error)
    {
        Error = error;
        IsSuccess = error is null;
    }

    /// <summary>Gets a value indicating whether the operation succeeded.</summary>
    public bool IsSuccess { get; }

    /// <summary>Gets a value indicating whether the operation failed.</summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>Gets the error when <see cref="IsFailure"/> is true; otherwise null.</summary>
    public Error? Error { get; }

    /// <summary>Converts an error into a failed result.</summary>
    /// <param name="failure">The failure.</param>
    public static implicit operator Result(Error failure) => FromError(failure);

    /// <summary>Creates a failed result from <paramref name="failure"/>.</summary>
    /// <param name="failure">The failure.</param>
    /// <returns>A failed result.</returns>
    public static Result FromError(Error failure) => Failure(failure);

    /// <summary>Creates a successful result with no payload.</summary>
    /// <returns>A successful result.</returns>
    public static Result Success() => new(null);

    /// <summary>Creates a successful result that carries <paramref name="item"/>.</summary>
    /// <typeparam name="T">Payload type.</typeparam>
    /// <param name="item">Payload.</param>
    /// <returns>A successful result.</returns>
    public static Result<T> Success<T>(T item) => new(item, null);

    /// <summary>Creates a failed result.</summary>
    /// <param name="failure">The failure.</param>
    /// <returns>A failed result.</returns>
    public static Result Failure(Error failure)
    {
        ArgumentNullException.ThrowIfNull(failure);
        return new(failure);
    }

    /// <summary>Creates a failed result with a default payload.</summary>
    /// <typeparam name="T">Payload type.</typeparam>
    /// <param name="failure">The failure.</param>
    /// <returns>A failed result.</returns>
    public static Result<T> Failure<T>(Error failure)
    {
        ArgumentNullException.ThrowIfNull(failure);
        return new(default, failure);
    }
}
