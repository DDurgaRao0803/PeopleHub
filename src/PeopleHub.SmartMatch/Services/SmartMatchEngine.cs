using PeopleHub.Domain.Aggregates.Provider;
using PeopleHub.Domain.Entities;
using PeopleHub.SmartMatch.Interfaces;
using PeopleHub.SmartMatch.Models;
using PeopleHub.SmartMatch.Ranking;
using PeopleHub.SmartMatch.Rules;

namespace PeopleHub.SmartMatch.Services;

public sealed class SmartMatchEngine : ISmartMatchEngine
{
    private readonly VerificationRule _verificationRule = new();
    private readonly SkillRule _skillRule = new();
    private readonly AvailabilityRule _availabilityRule = new();
    private readonly ScoreCalculator _scoreCalculator;

    public SmartMatchEngine(
        ScoreCalculator scoreCalculator)
    {
        _scoreCalculator = scoreCalculator;
    }

    public SmartMatchResult FindBestMatch(
        ServiceRequest serviceRequest,
        IReadOnlyCollection<ProviderProfile> providers)
    {
        ArgumentNullException.ThrowIfNull(serviceRequest);
        ArgumentNullException.ThrowIfNull(providers);

        var scores = new List<ProviderScore>();

        ProviderProfile? selectedProvider = null;
        decimal highestScore = decimal.MinValue;

        foreach (var provider in providers)
        {
            if (!_verificationRule.IsEligible(provider))
            {
                continue;
            }

            if (!_skillRule.IsEligible(
                provider,
                serviceRequest.ServiceCategoryId))
            {
                continue;
            }

            if (!_availabilityRule.IsEligible(
                provider,
                serviceRequest.RequestedDate))
            {
                continue;
            }

            var score = _scoreCalculator.Calculate(
                new ScoreContext
                {
                    Provider = provider
                });

            scores.Add(score);

            if (score.Score > highestScore)
            {
                highestScore = score.Score;
                selectedProvider = provider;
            }
        }

        return new SmartMatchResult
        {
            SelectedProvider = selectedProvider,
            Scores = scores
        };
    }
}