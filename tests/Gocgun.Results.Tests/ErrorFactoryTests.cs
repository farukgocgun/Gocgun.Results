namespace Gocgun.Results.Tests;

/// <summary>Each factory sets the matching error type.</summary>
public sealed class ErrorFactoryTests
{
    /// <summary>Factories cover every <see cref="ErrorType"/> value.</summary>
    [Fact]
    public void ErrorFactories_SetMatchingErrorType()
    {
        IReadOnlyDictionary<string, string[]> details = new Dictionary<string, string[]>(StringComparer.Ordinal) { ["name"] = ["Required"] };
        IReadOnlyDictionary<string, object?> metadata = new Dictionary<string, object?>(StringComparer.Ordinal) { ["max"] = 1 };

        Assert.Equal(ErrorType.Validation, Error.Validation("Validation", "m", details).Type);
        Assert.Equal(details, Error.Validation("Validation", "m", details).Details);
        Assert.Equal(ErrorType.Unauthorized, Error.Unauthorized("InvalidCredentials", "m").Type);
        Assert.Equal(ErrorType.Forbidden, Error.Forbidden("EmailNotConfirmed", "m").Type);
        Assert.Equal(ErrorType.NotFound, Error.NotFound("Posts.NotFound", "m").Type);
        Assert.Equal(ErrorType.PaymentRequired, Error.PaymentRequired("PlanLimitExceeded", "m", metadata).Type);
        Assert.Equal(metadata, Error.PaymentRequired("PlanLimitExceeded", "m", metadata).Metadata);
        Assert.Equal(ErrorType.Conflict, Error.Conflict("Slots.SlotOccupied", "m").Type);
        Assert.Equal(ErrorType.Concurrency, Error.Concurrency("ConcurrencyConflict", "m").Type);
        Assert.Equal(ErrorType.BusinessRule, Error.BusinessRule("NoEligibleSlot", "m").Type);
        Assert.Equal(ErrorType.UnsupportedMediaType, Error.UnsupportedMediaType("InvalidThumbnail", "m").Type);
        Assert.Equal(ErrorType.RateLimited, Error.RateLimited("RateLimited", "m", metadata).Type);
        Assert.Equal(ErrorType.ServiceUnavailable, Error.ServiceUnavailable("Ai.ProviderNotConfigured", "m").Type);
        Assert.Equal(11, Enum.GetValues<ErrorType>().Length);
    }
}
