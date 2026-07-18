namespace PeopleHub.Contracts.Users;

public sealed record UpdateUserRequest(
    string FirstName,
    string LastName);