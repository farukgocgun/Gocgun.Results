namespace Gocgun.Results;

/// <summary>Creates a failed result when the response type is known only as a generic constraint.</summary>
public static class ResultFactory
{
    /// <summary>Creates a failed <typeparamref name="TResponse"/>.</summary>
    /// <typeparam name="TResponse">A result type with a static failure factory.</typeparam>
    /// <param name="error">The failure.</param>
    /// <returns>A failed result.</returns>
    public static TResponse Failure<TResponse>(Error error)
        where TResponse : Result, IResultFactory<TResponse>
    {
        ArgumentNullException.ThrowIfNull(error);
        return TResponse.Failure(error);
    }
}
