namespace Gocgun.Results;

/// <summary>
/// Static failure factory for a <see cref="Result"/> subtype. Pipeline code calls
/// <see cref="ResultFactory.Failure{TResponse}"/> without reflection.
/// </summary>
/// <typeparam name="TSelf">The result type that implements this factory.</typeparam>
public interface IResultFactory<out TSelf>
    where TSelf : Result
{
    /// <summary>Creates a failed result of <typeparamref name="TSelf"/>.</summary>
    /// <param name="failure">The failure. Must not be null.</param>
    /// <returns>A failed result.</returns>
    static abstract TSelf Failure(Error failure);
}
