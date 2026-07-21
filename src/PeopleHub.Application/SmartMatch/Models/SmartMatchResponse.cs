namespace PeopleHub.Application.SmartMatch.Models;

public sealed class SmartMatchResponse
{
    public Guid? SelectedProviderId { get; init; }

    public int CandidateCount { get; init; }
}