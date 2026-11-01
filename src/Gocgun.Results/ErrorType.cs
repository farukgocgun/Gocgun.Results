namespace Gocgun.Results;

/// <summary>
/// Category of an application failure. The HTTP mapper uses this value, never the code string.
/// </summary>
public enum ErrorType
{
    /// <summary>Request fails rules checkable from the request. Status 400.</summary>
    Validation = 0,

    /// <summary>Caller is not authenticated. Status 401.</summary>
    Unauthorized,

    /// <summary>Caller is authenticated but not allowed. Status 403.</summary>
    Forbidden,

    /// <summary>Target does not exist or must not be disclosed. Status 404.</summary>
    NotFound,

    /// <summary>Plan limit, credits, feature, or payment state. Status 402.</summary>
    PaymentRequired,

    /// <summary>Request collides with an existing resource. Status 409.</summary>
    Conflict,

    /// <summary>Optimistic concurrency token is stale. Status 409.</summary>
    Concurrency,

    /// <summary>Domain rule makes a valid request impossible. Status 422.</summary>
    BusinessRule,

    /// <summary>Body content type is not accepted. Status 415.</summary>
    UnsupportedMediaType,

    /// <summary>Rate limit hit. Status 429.</summary>
    RateLimited,

    /// <summary>A required backing service or provider is not available or not configured. Status 503.</summary>
    ServiceUnavailable,
}
