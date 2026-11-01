namespace Gocgun.Results.Tests;

/// <summary>Locks the ADR 0006 result contract.</summary>
public sealed class ResultTests
{
    /// <summary>A successful result has no error.</summary>
    [Fact]
    public void Success_is_success()
    {
        var result = Result.Success();
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Error);
    }

    /// <summary>Success carries the item and no error.</summary>
    [Fact]
    public void Success_HasItem_AndNoError()
    {
        var result = Result.Success(42);
        Assert.True(result.IsSuccess);
        Assert.Null(result.Error);
        Assert.Equal(42, result.Item);
    }

    /// <summary>Failure exposes the error and a default item.</summary>
    [Fact]
    public void Failure_HasError_AndDefaultItem()
    {
        var error = Error.NotFound("Posts.NotFound", "Post was not found.");
        var result = Result.Failure<int>(error);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
        Assert.Equal(0, result.Item);
    }

    /// <summary>A null error is rejected.</summary>
    [Fact]
    public void Failure_WithNullError_Throws() =>
        Assert.Throws<ArgumentNullException>(() => Result.Failure(null!));

    /// <summary>An error converts to a failed result.</summary>
    [Fact]
    public void Implicit_error_conversion_fails()
    {
        Result result = Error.Conflict("Slots.SlotOccupied", "Slot is occupied.");
        Assert.True(result.IsFailure);
        Assert.Equal(ErrorType.Conflict, result.Error!.Type);
    }

    /// <summary>A value converts to a successful result.</summary>
    [Fact]
    public void Implicit_conversion_succeeds()
    {
        Result<string> result = "ok";
        Assert.True(result.IsSuccess);
        Assert.Equal("ok", result.Item);
    }

    /// <summary>When the payload type is Error, callers use the factories.</summary>
    [Fact]
    public void Payload_of_error_uses_explicit_factories()
    {
        var error = Error.Validation("Validation", "Invalid.");
        var success = Result.Success(error);
        var failure = Result<Error>.Failure(error);
        Assert.True(success.IsSuccess);
        Assert.Equal(error, success.Item);
        Assert.True(failure.IsFailure);
        Assert.Null(failure.Item);
    }

    /// <summary>The generic factory dispatches to the matching type.</summary>
    [Fact]
    public void ResultFactory_Failure_matches_type()
    {
        var error = Error.BusinessRule("ConditionNotSupported", "Not supported.");
        var bare = ResultFactory.Failure<Result>(error);
        var valued = ResultFactory.Failure<Result<int>>(error);
        var page = ResultFactory.Failure<ResultPaginated<int>>(error);
        Assert.True(bare.IsFailure);
        Assert.Equal(0, valued.Item);
        Assert.Empty(page.Item);
        Assert.Null(page.Total);
    }
}
