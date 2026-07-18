using System.Net.Http.Headers;

namespace PeopleHub.IntegrationTests.Infrastructure;

public static class AuthenticationHelper
{
    public static void SetBearerToken(HttpClient client, string token)
    {
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }
}