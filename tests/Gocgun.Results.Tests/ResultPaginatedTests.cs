namespace Gocgun.Results.Tests;

/// <summary>Page contract.</summary>
public sealed class ResultPaginatedTests
{
    /// <summary>An empty page is success without a total.</summary>
    [Fact]
    public void Paginated_EmptyList_IsSuccess()
    {
        var page = ResultPaginated<int>.Success([]);
        Assert.True(page.IsSuccess);
        Assert.Empty(page.Item);
        Assert.Null(page.Total);
        Assert.Null(page.Error);
    }

    /// <summary>A page carries its rows and an optional total.</summary>
    [Fact]
    public void Paginated_with_total_keeps_rows_and_total()
    {
        var page = ResultPaginated<string>.Success(["a"], 42);
        Assert.Equal(["a"], page.Item);
        Assert.Equal(42, page.Total);
    }
}
