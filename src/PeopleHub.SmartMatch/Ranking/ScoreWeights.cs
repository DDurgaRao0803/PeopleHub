namespace PeopleHub.SmartMatch.Ranking;

internal static class ScoreWeights
{
    public const decimal Rating = 30m;

    public const decimal Experience = 15m;

    public const decimal CompletedJobs = 15m;

    public const decimal Verification = 15m;

    public const decimal ResponseRate = 10m;

    public const decimal Distance = 15m;
}