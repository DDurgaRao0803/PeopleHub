using Xunit;

namespace PeopleHub.IntegrationTests.Infrastructure;

[CollectionDefinition("Integration Tests")]
public sealed class IntegrationTestCollection
    : ICollectionFixture<CustomWebApplicationFactory>
{
}