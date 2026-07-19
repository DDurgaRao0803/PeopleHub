namespace PeopleHub.IntegrationTests.Infrastructure;

[Collection("Integration Tests")]
public abstract class IntegrationTestBase
{
    protected readonly HttpClient Client;
    protected readonly CustomWebApplicationFactory Factory;

    protected IntegrationTestBase(CustomWebApplicationFactory factory)
{
    Factory = factory;

    Factory.ResetDatabase();

    Client = factory.CreateClient();
}
}