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
        IReadOnlyCollection<SmartMatchCandidate> candidates)
    {
        ArgumentNullException.ThrowIfNull(serviceRequest);
        ArgumentNullException.ThrowIfNull(candidates);


        var scores = new List<ProviderScore>();

        SmartMatchCandidate? selectedCandidate = null;

        decimal highestScore = decimal.MinValue;



        foreach (var candidate in candidates)
        {
            var provider = candidate.Provider;


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



            var providerScore =
                _scoreCalculator.Calculate(
                    new ScoreContext
                    {
                        Provider = provider
                    });



            /*
             * Bid adjustment:
             *
             * Lower bid amount improves score.
             * Faster completion improves score.
             */


            var bidScore =
                CalculateBidScore(
                    candidate);



            var finalScore =
                providerScore.Score +
                bidScore;



            scores.Add(
                new ProviderScore
                {
                    ProviderId = provider.Id,
                    Score = finalScore
                });



            if (finalScore > highestScore)
            {
                highestScore = finalScore;
                selectedCandidate = candidate;
            }
        }



        return new SmartMatchResult
        {
            SelectedProvider =
                selectedCandidate?.Provider,

            Scores = scores
        };
    }



    private static decimal CalculateBidScore(
        SmartMatchCandidate candidate)
    {
        var bid = candidate.Bid;


        /*
         * Example weighting:
         *
         * Price:
         * lower amount = better
         *
         * Time:
         * lower minutes = better
         */


        var priceScore =
            100m / bid.Amount;


        var timeScore =
            100m / bid.EstimatedMinutes;


        return priceScore + timeScore;
    }
}