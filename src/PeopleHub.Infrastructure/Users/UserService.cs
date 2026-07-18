using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Users;
using PeopleHub.Contracts.Common;
using PeopleHub.Contracts.Users;

namespace PeopleHub.Infrastructure.Users;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CurrentUserResponse> GetCurrentUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(
            userId,
            cancellationToken);

        if (user is null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        return new CurrentUserResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email.Value,
            user.Role.ToString());
    }

    public async Task<UserResponse> GetUserByIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(
            userId,
            cancellationToken);

        if (user is null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        return new UserResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email.Value,
            user.Role.ToString());
    }

    public async Task<PagedResponse<UserResponse>> GetAllUsersAsync(
    GetUsersRequest request,
    CancellationToken cancellationToken = default)
    {
        var (users, totalCount) = await _userRepository.GetPagedAsync(
    request,
    cancellationToken);

        var items = users
            .Select(user => new UserResponse(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email.Value,
                user.Role.ToString()))
            .ToList();

        return new PagedResponse<UserResponse>(
    items,
    request.PageNumber,
    request.PageSize,
    totalCount);
    }

    public async Task<UserResponse> UpdateUserAsync(
        Guid userId,
        UpdateUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(
            userId,
            cancellationToken);

        if (user is null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        user.ChangeFirstName(request.FirstName);
        user.ChangeLastName(request.LastName);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new UserResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email.Value,
            user.Role.ToString());
    }

    public async Task DeleteUserAsync(
    Guid userId,
    CancellationToken cancellationToken = default)
{
    var user = await _userRepository.GetByIdAsync(
        userId,
        cancellationToken);

    if (user is null)
    {
        throw new KeyNotFoundException("User not found.");
    }

    user.MarkAsDeleted();

    await _unitOfWork.SaveChangesAsync(cancellationToken);
}

}