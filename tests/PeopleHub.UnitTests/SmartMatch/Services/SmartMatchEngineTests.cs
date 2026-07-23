using PeopleHub.Domain.Aggregates.Bidding;
using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Domain.Entities;
using PeopleHub.SmartMatch.Models;
using PeopleHub.SmartMatch.Services;
using PeopleHub.SmartMatch.Ranking;
using PeopleHub.SmartMatch.Ranking.Interfaces;
using PeopleHub.SmartMatch.Ranking.Rules;

namespace PeopleHub.UnitTests.SmartMatch.Services;

public class SmartMatchEngineTests
{
    [Fact]
    public void FindBestMatch_ReturnsHighestScoringEligibleProvider()
    {
        // Arrange
        var category = new ServiceCategory("Electrical");


        var provider1 = new ProviderProfile(
            Guid.NewGuid(),
            "Junior Electrician",
            5);

        provider1.Approve();

        provider1.AddSkill(category);

        provider1.AddAvailability(
            DayOfWeek.Monday,
            new TimeOnly(9, 0),
            new TimeOnly(17, 0));



        var provider2 = new ProviderProfile(
            Guid.NewGuid(),
            "Senior Electrician",
            15);

        provider2.Approve();

        provider2.AddSkill(category);

        provider2.AddAvailability(
            DayOfWeek.Monday,
            new TimeOnly(9, 0),
            new TimeOnly(17, 0));



        var serviceRequest = new ServiceRequest(
            Guid.NewGuid(),
            category.Id,
            "Electrical Repair",
            "Repair electrical wiring",
            new DateTime(2026, 7, 20, 10, 0, 0));



        var bid1 = new ProviderBid(
            serviceRequest.Id,
            provider1.Id,
            200,
            60);



        var bid2 = new ProviderBid(
            serviceRequest.Id,
            provider2.Id,
            150,
            30);



        var candidates =
            new List<SmartMatchCandidate>
            {
                new()
                {
                    Provider = provider1,
                    Bid = bid1
                },

                new()
                {
                    Provider = provider2,
                    Bid = bid2
                }
            };



        var scoreCalculator = new ScoreCalculator(
            new IScoreRule[]
            {
                new ExperienceScoreRule(),
                new RatingScoreRule(),
                new CompletedJobsScoreRule(),
                new VerificationScoreRule(),
                new ResponseRateScoreRule()
            });



        var engine = new SmartMatchEngine(
            scoreCalculator);



        // Act

        var result =
            engine.FindBestMatch(
                serviceRequest,
                candidates);



        // Assert

        Assert.NotNull(result.SelectedProvider);

        Assert.Equal(
            provider2.Id,
            result.SelectedProvider!.Id);


        Assert.Equal(
            2,
            result.Scores.Count);
    }



    [Fact]
    public void FindBestMatch_ReturnsNull_WhenNoProviderIsEligible()
    {
        // Arrange

        var category =
            new ServiceCategory("Electrical");



        var provider =
            new ProviderProfile(
                Guid.NewGuid(),
                "Electrician",
                10);



        var serviceRequest =
            new ServiceRequest(
                Guid.NewGuid(),
                category.Id,
                "Electrical Repair",
                "Repair electrical wiring",
                new DateTime(2026, 7, 20, 10, 0, 0));



        var bid =
            new ProviderBid(
                serviceRequest.Id,
                provider.Id,
                100,
                30);



        var candidates =
            new List<SmartMatchCandidate>
            {
                new()
                {
                    Provider = provider,
                    Bid = bid
                }
            };



        var scoreCalculator =
            new ScoreCalculator(
                new IScoreRule[]
                {
                    new ExperienceScoreRule(),
                    new RatingScoreRule(),
                    new CompletedJobsScoreRule(),
                    new VerificationScoreRule(),
                    new ResponseRateScoreRule()
                });



        var engine =
            new SmartMatchEngine(
                scoreCalculator);



        // Act

        var result =
            engine.FindBestMatch(
                serviceRequest,
                candidates);



        // Assert

        Assert.Null(
            result.SelectedProvider);


        Assert.Empty(
            result.Scores);
    }
}