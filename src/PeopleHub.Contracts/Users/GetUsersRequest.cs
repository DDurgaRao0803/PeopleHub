namespace PeopleHub.Contracts.Users;

public sealed record GetUsersRequest(
    int PageNumber = 1,
    int PageSize = 10,
    string? FirstName = null,
    string? LastName = null,
    string? Email = null,
    string? Status = null,
    string? Role = null,
    string? SortBy = "FirstName",
    string? SortDirection = "Ascending");