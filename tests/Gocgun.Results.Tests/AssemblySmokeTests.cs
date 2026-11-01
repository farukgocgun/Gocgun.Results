using System.Reflection;

namespace Gocgun.Results.Tests;

/// <summary>The package assembly loads and has no web or data framework reference.</summary>
public sealed class AssemblySmokeTests
{
    /// <summary>Referenced assemblies stay on the BCL.</summary>
    [Fact]
    public void Assembly_has_no_http_or_ef_reference()
    {
        var names = typeof(Result).Assembly.GetReferencedAssemblies().Select(a => a.Name).ToArray();
        Assert.DoesNotContain(names, name => name is not null && (
            name.Contains("AspNetCore", StringComparison.Ordinal)
            || name.Contains("EntityFramework", StringComparison.Ordinal)
            || name.Contains("Http", StringComparison.Ordinal)));
        Assert.NotNull(Assembly.GetExecutingAssembly());
    }
}
