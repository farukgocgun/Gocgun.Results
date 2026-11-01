namespace Gocgun.Results;

/// <summary>
/// Application error carried by a failed <see cref="Result"/>.
/// </summary>
/// <param name="Code">Stable code clients branch on.</param>
/// <param name="Message">Display text. Localization stays with the caller.</param>
/// <param name="Type">Category mapped to an HTTP status outside this package.</param>
/// <param name="Details">Field errors. Keys are field names.</param>
/// <param name="Metadata">Extension data such as limit, max, used, or retryAfterSeconds.</param>
/// <remarks>
/// Record equality compares <paramref name="Details"/> and <paramref name="Metadata"/> by reference, so two errors built from separate but identical dictionaries are not equal.
/// </remarks>
public sealed record Error(
    string Code,
    string Message,
    ErrorType Type,
    IReadOnlyDictionary<string, string[]>? Details = null,
    IReadOnlyDictionary<string, object?>? Metadata = null)
{
    /// <summary>
    /// Gets the stable code clients branch on. Validated on every assignment, so <c>with { Code = "" }</c> throws.
    /// </summary>
    public string Code
    {
        get;
        init
        {
            field = RequireText(value);
        }
    } = RequireText(Code);

    /// <summary>
    /// Gets the display text. Validated on every assignment, so <c>with { Message = null! }</c> throws.
    /// </summary>
    public string Message
    {
        get;
        init
        {
            field = RequireText(value);
        }
    } = RequireText(Message);

    /// <summary>
    /// Gets the category. Validated on every assignment, so <c>with { Type = (ErrorType)99 }</c> throws.
    /// </summary>
    public ErrorType Type
    {
        get;
        init
        {
            field = RequireType(value);
        }
    } = RequireType(Type);

    /// <summary>Validation failure, optionally with field errors.</summary>
    /// <param name="code">Stable error code.</param>
    /// <param name="message">Display message.</param>
    /// <param name="details">Field errors.</param>
    /// <returns>A validation error.</returns>
    public static Error Validation(string code, string message, IReadOnlyDictionary<string, string[]>? details = null) =>
        new(code, message, ErrorType.Validation, details);

    /// <summary>Missing or invalid credentials.</summary>
    /// <param name="code">Stable error code.</param>
    /// <param name="message">Display message.</param>
    /// <returns>An unauthorized error.</returns>
    public static Error Unauthorized(string code, string message) =>
        new(code, message, ErrorType.Unauthorized);

    /// <summary>Authenticated caller is not allowed to perform the action.</summary>
    /// <param name="code">Stable error code.</param>
    /// <param name="message">Display message.</param>
    /// <returns>A forbidden error.</returns>
    public static Error Forbidden(string code, string message) =>
        new(code, message, ErrorType.Forbidden);

    /// <summary>Target is missing or must not be disclosed.</summary>
    /// <param name="code">Stable error code.</param>
    /// <param name="message">Display message.</param>
    /// <returns>A not-found error.</returns>
    public static Error NotFound(string code, string message) =>
        new(code, message, ErrorType.NotFound);

    /// <summary>Plan, credit, or payment state blocks the action.</summary>
    /// <param name="code">Stable error code.</param>
    /// <param name="message">Display message.</param>
    /// <param name="metadata">Limit, max, used, or feature members.</param>
    /// <returns>A payment-required error.</returns>
    public static Error PaymentRequired(string code, string message, IReadOnlyDictionary<string, object?>? metadata = null) =>
        new(code, message, ErrorType.PaymentRequired, Metadata: metadata);

    /// <summary>Request collides with the current state of a resource.</summary>
    /// <param name="code">Stable error code.</param>
    /// <param name="message">Display message.</param>
    /// <returns>A conflict error.</returns>
    public static Error Conflict(string code, string message) =>
        new(code, message, ErrorType.Conflict);

    /// <summary>Optimistic concurrency token is stale.</summary>
    /// <param name="code">Stable error code.</param>
    /// <param name="message">Display message.</param>
    /// <returns>A concurrency error.</returns>
    public static Error Concurrency(string code, string message) =>
        new(code, message, ErrorType.Concurrency);

    /// <summary>A domain rule makes the request impossible.</summary>
    /// <param name="code">Stable error code.</param>
    /// <param name="message">Display message.</param>
    /// <returns>A business-rule error.</returns>
    public static Error BusinessRule(string code, string message) =>
        new(code, message, ErrorType.BusinessRule);

    /// <summary>Uploaded body content type is not accepted.</summary>
    /// <param name="code">Stable error code.</param>
    /// <param name="message">Display message.</param>
    /// <returns>An unsupported-media-type error.</returns>
    public static Error UnsupportedMediaType(string code, string message) =>
        new(code, message, ErrorType.UnsupportedMediaType);

    /// <summary>Rate limit was hit.</summary>
    /// <param name="code">Stable error code.</param>
    /// <param name="message">Display message.</param>
    /// <param name="metadata">Retry metadata such as retryAfterSeconds.</param>
    /// <returns>A rate-limited error.</returns>
    public static Error RateLimited(string code, string message, IReadOnlyDictionary<string, object?>? metadata = null) =>
        new(code, message, ErrorType.RateLimited, Metadata: metadata);

    /// <summary>A required backing service or provider is not available or not configured.</summary>
    /// <param name="code">Stable error code.</param>
    /// <param name="message">Display message.</param>
    /// <returns>A service-unavailable error.</returns>
    public static Error ServiceUnavailable(string code, string message) =>
        new(code, message, ErrorType.ServiceUnavailable);

    private static string RequireText(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        return value;
    }

    private static ErrorType RequireType(ErrorType value) =>
        Enum.IsDefined(value)
            ? value
            : throw new ArgumentOutOfRangeException(nameof(value), value, "Error type is not defined.");
}
