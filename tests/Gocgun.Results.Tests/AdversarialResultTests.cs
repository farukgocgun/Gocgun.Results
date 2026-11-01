namespace Gocgun.Results.Tests;

/// <summary>Rejects empty codes and undefined error types.</summary>
public sealed class AdversarialResultTests
{
    /// <summary>An empty or blank code is a caller error.</summary>
    [Fact]
    public void Empty_code_throws()
    {
        Assert.Throws<ArgumentException>(() => Error.NotFound(string.Empty, "missing"));
        Assert.Throws<ArgumentException>(() => Error.Conflict("   ", "occupied"));
    }

    /// <summary>A blank message is a caller error.</summary>
    [Fact]
    public void Blank_message_throws() =>
        Assert.Throws<ArgumentException>(() => Error.Validation("Validation", " "));

    /// <summary>An undefined enum value is rejected.</summary>
    [Fact]
    public void Undefined_error_type_throws() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => new Error("Code", "Message", (ErrorType)999));

    /// <summary>Validation also applies to <c>with</c> expressions.</summary>
    [Fact]
    public void With_expression_is_validated()
    {
        var error = Error.NotFound("Code", "Message");
        Assert.Throws<ArgumentException>(() => error with { Code = " " });
        Assert.ThrowsAny<ArgumentException>(() => error with { Message = null! });
        Assert.Throws<ArgumentOutOfRangeException>(() => error with { Type = (ErrorType)99 });
        Assert.Equal("Other", (error with { Code = "Other" }).Code);
    }

    /// <summary>A null error still fails the result factory.</summary>
    [Fact]
    public void Null_error_on_paginated_failure_throws() =>
        Assert.Throws<ArgumentNullException>(() => ResultPaginated<string>.Failure(null!));
}
