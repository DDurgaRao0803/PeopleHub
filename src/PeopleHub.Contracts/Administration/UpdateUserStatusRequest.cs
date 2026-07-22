namespace PeopleHub.Contracts.Administration;

public sealed class UpdateUserStatusRequest
{
    public string Status { get; init; } = string.Empty;
}