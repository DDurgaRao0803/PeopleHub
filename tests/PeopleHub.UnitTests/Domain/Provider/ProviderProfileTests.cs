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
}