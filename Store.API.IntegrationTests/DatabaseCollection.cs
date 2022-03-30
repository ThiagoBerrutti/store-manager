using StoreAPI;
using Xunit;

namespace Store.API.IntegrationTests
{
    [CollectionDefinition("tests")]
    public class DatabaseCollection : ICollectionFixture<TestWebApplicationFactory<Startup>>
    {
    }
}