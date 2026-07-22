using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Domain.Enums;

namespace PeopleHub.UnitTests.Domain.Entities;

public class ProviderProfileTests
{
    [Fact]
    public void Constructor_Should_Set_NotSubmitted_Status()
    {
        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Experienced electrician",
            5);

        Assert.Equal(VerificationStatus.NotSubmitted, provider.VerificationStatus);
    }

    [Fact]
    public void Approve_Should_Set_Status_To_Approved()
    {
        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Experienced electrician",
            5);

        provider.Approve();

        Assert.Equal(VerificationStatus.Approved, provider.VerificationStatus);
    }

    [Fact]
    public void Reject_Should_Set_Status_To_Rejected()
    {
        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Experienced electrician",
            5);

        provider.Reject();

        Assert.Equal(VerificationStatus.Rejected, provider.VerificationStatus);
    }

    [Fact]
    public void UpdateBio_Should_Change_Bio()
    {
        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Experienced electrician",
            5);

        provider.UpdateBio("Licensed electrician");

        Assert.Equal("Licensed electrician", provider.Bio);
    }

    [Fact]
    public void UpdateExperience_Should_Change_Experience()
    {
        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Experienced electrician",
            5);

        provider.UpdateExperience(10);

        Assert.Equal(10, provider.ExperienceYears);
    }

    [Fact]
    public void AddSkill_Should_Add_New_Skill()
    {
        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Experienced electrician",
            5);

        var category = new ServiceCategory("Electrical");

        provider.AddSkill(category);

        Assert.Single(provider.Skills);
    }

    [Fact]
    public void AddSkill_Should_Not_Add_Duplicate_Skill()
    {
        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Experienced electrician",
            5);

        var category = new ServiceCategory("Electrical");

        provider.AddSkill(category);
        provider.AddSkill(category);

        Assert.Single(provider.Skills);
    }

    [Fact]
    public void RemoveSkill_Should_Remove_Skill()
    {
        var provider = new ProviderProfile(
            Guid.NewGuid(),
            "Experienced electrician",
            5);

        var category = new ServiceCategory("Electrical");

        provider.AddSkill(category);
        provider.RemoveSkill(category.Id);

        Assert.Empty(provider.Skills);
    }

    [Fact]
public void Constructor_Should_Initialize_SmartMatch_Metrics()
{
    var provider = new ProviderProfile(
        Guid.NewGuid(),
        "Experienced electrician",
        5);

    Assert.Equal(0m, provider.AverageRating);
    Assert.Equal(0, provider.CompletedJobs);
    Assert.Equal(0m, provider.ResponseRate);
    Assert.True(provider.LastActiveUtc <= DateTime.UtcNow);
}

[Fact]
public void UpdateRating_Should_Change_Rating()
{
    var provider = new ProviderProfile(
        Guid.NewGuid(),
        "Experienced electrician",
        5);

    provider.UpdateRating(4.8m);

    Assert.Equal(4.8m, provider.AverageRating);
}

[Fact]
public void UpdateRating_Should_Throw_When_Rating_Is_Invalid()
{
    var provider = new ProviderProfile(
        Guid.NewGuid(),
        "Experienced electrician",
        5);

    Assert.Throws<ArgumentOutOfRangeException>(
        () => provider.UpdateRating(5.5m));
}

[Fact]
public void IncrementCompletedJobs_Should_Increase_Count()
{
    var provider = new ProviderProfile(
        Guid.NewGuid(),
        "Experienced electrician",
        5);

    provider.IncrementCompletedJobs();
    provider.IncrementCompletedJobs();

    Assert.Equal(2, provider.CompletedJobs);
}

[Fact]
public void UpdateResponseRate_Should_Change_ResponseRate()
{
    var provider = new ProviderProfile(
        Guid.NewGuid(),
        "Experienced electrician",
        5);

    provider.UpdateResponseRate(92m);

    Assert.Equal(92m, provider.ResponseRate);
}

[Fact]
public void UpdateResponseRate_Should_Throw_When_Invalid()
{
    var provider = new ProviderProfile(
        Guid.NewGuid(),
        "Experienced electrician",
        5);

    Assert.Throws<ArgumentOutOfRangeException>(
        () => provider.UpdateResponseRate(120m));
}

[Fact]
public void UpdateLastActive_Should_Update_LastActiveUtc()
{
    var provider = new ProviderProfile(
        Guid.NewGuid(),
        "Experienced electrician",
        5);

    var previous = provider.LastActiveUtc;

    Thread.Sleep(20);

    provider.UpdateLastActive();

    Assert.True(provider.LastActiveUtc > previous);
}

[Fact]
public void UpdateLocation_Should_Set_Latitude_And_Longitude()
{
    var provider = new ProviderProfile(
        Guid.NewGuid(),
        "Experienced electrician",
        5);

    provider.UpdateLocation(17.3850m, 78.4867m);

    Assert.Equal(17.3850m, provider.Latitude);
    Assert.Equal(78.4867m, provider.Longitude);
}

[Fact]
public void UpdateLocation_Should_Throw_When_Latitude_Is_Invalid()
{
    var provider = new ProviderProfile(
        Guid.NewGuid(),
        "Experienced electrician",
        5);

    Assert.Throws<ArgumentOutOfRangeException>(
        () => provider.UpdateLocation(95m, 78m));
}

[Fact]
public void UpdateLocation_Should_Throw_When_Longitude_Is_Invalid()
{
    var provider = new ProviderProfile(
        Guid.NewGuid(),
        "Experienced electrician",
        5);

    Assert.Throws<ArgumentOutOfRangeException>(
        () => provider.UpdateLocation(17m, 190m));
}

[Fact]
public void UpdateLocation_Should_Allow_Boundary_Values()
{
    var provider = new ProviderProfile(
        Guid.NewGuid(),
        "Experienced electrician",
        5);

    provider.UpdateLocation(-90m, 180m);

    Assert.Equal(-90m, provider.Latitude);
    Assert.Equal(180m, provider.Longitude);
}



}