using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace WordFinderAPI.IntegrationTest;

public class TestingWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public Task InitializeAsync()
    {
        throw new NotImplementedException();
    }

    public Task DisposeAsync()
    {
        throw new NotImplementedException();
    }
}