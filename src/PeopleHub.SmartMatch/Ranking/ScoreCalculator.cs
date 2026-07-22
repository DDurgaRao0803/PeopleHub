using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Domain.Enums;
using PeopleHub.SmartMatch.Models;

namespace PeopleHub.SmartMatch.Ranking;

public sealed class ScoreCalculator
{
    private const decimal MaxExperienceYears = 20m;
    private const decimal MaxCompletedJobs = 100m;

    private const decimal ExperienceWeight = 25m;
    private const decimal RatingWeight = 30m;
    private const decimal CompletedJobsWeight = 20m;
    private const decimal VerificationWeight = 10m;
    private const decimal ResponseRateWeight = 15m;

    public ProviderScore Calculate(ProviderProfile provider)
    {
        ArgumentNullException.ThrowIfNull(provider);

        decimal experienceScore =
            Math.Min(provider.ExperienceYears, MaxExperienceYears)
            / MaxExperienceYears
            * ExperienceWeight;

        decimal ratingScore =
            provider.AverageRating
            / 5m
            * RatingWeight;

        decimal completedJobsScore =
            Math.Min(provider.CompletedJobs, MaxCompletedJobs)
            / MaxCompletedJobs
            * CompletedJobsWeight;

        decimal verificationScore =
            provider.VerificationStatus == VerificationStatus.Approved
                ? VerificationWeight
                : 0m;

        decimal responseRateScore =
            provider.ResponseRate
            / 100m
            * ResponseRateWeight;

        decimal totalScore =
            experienceScore +
            ratingScore +
            completedJobsScore +
            verificationScore +
            responseRateScore;

        return new ProviderScore
        {
            ProviderId = provider.Id,
            Score = totalScore
        };
    }
}