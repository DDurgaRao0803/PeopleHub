using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using PeopleHub.API.Controllers;
using PeopleHub.Application.Providers;
using PeopleHub.Contracts.Providers;

namespace PeopleHub.UnitTests;

public class ProviderProfilesControllerTests
{
    [Fact]
public async Task Get_ReturnsNotFound_WhenProfileDoesNotExist()
{
    var controller = new ProviderProfilesController(
        new ProfileNotFoundService());

    controller.ControllerContext = new ControllerContext
    {
        HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(
                new ClaimsIdentity(
                [
                    new Claim(
                        ClaimTypes.NameIdentifier,
                        Guid.NewGuid().ToString())
                ]))
        }
    };

    var result = await controller.Get(CancellationToken.None);

    Assert.IsType<NotFoundResult>(result.Result);
}
[Fact]
public async Task Create_ReturnsCreated_WhenProfileIsCreated()
{
    var controller = new ProviderProfilesController(
        new SuccessfulProviderProfileService());

    controller.ControllerContext = new ControllerContext
    {
        HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(
                new ClaimsIdentity(
                [
                    new Claim(
                        ClaimTypes.NameIdentifier,
                        Guid.NewGuid().ToString())
                ]))
        }
    };

    var request = new CreateProviderProfileRequest
    {
        Bio = "Experienced electrician",
        ExperienceYears = 8
    };

    var result = await controller.Create(
        request,
        CancellationToken.None);

    var created = Assert.IsType<CreatedAtActionResult>(result.Result);

    var response = Assert.IsType<ProviderProfileResponse>(created.Value);

    Assert.Equal(request.Bio, response.Bio);
    Assert.Equal(request.ExperienceYears, response.ExperienceYears);
}
private sealed class ProfileNotFoundService : IProviderProfileService
{
    public Task<ProviderProfileResponse> CreateAsync(
        Guid userId,
        CreateProviderProfileRequest request,
        CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public Task<ProviderProfileResponse?> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
        => Task.FromResult<ProviderProfileResponse?>(null);

    public Task<ProviderProfileResponse> UpdateAsync(
        Guid userId,
        UpdateProviderProfileRequest request,
        CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public Task DeleteAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
        => throw new NotImplementedException();
}
    


private sealed class SuccessfulProviderProfileService : IProviderProfileService
{
    public Task<ProviderProfileResponse> CreateAsync(
        Guid userId,
        CreateProviderProfileRequest request,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new ProviderProfileResponse
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Bio = request.Bio,
            ExperienceYears = request.ExperienceYears,
            VerificationStatus = "NotSubmitted"
        });
    }

    public Task<ProviderProfileResponse?> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public Task<ProviderProfileResponse> UpdateAsync(
        Guid userId,
        UpdateProviderProfileRequest request,
        CancellationToken cancellationToken = default)
        => throw new NotImplementedException();

    public Task DeleteAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
        => throw new NotImplementedException();
}
}