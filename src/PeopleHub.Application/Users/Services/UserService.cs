using PeopleHub.Application.Common.Interfaces.Persistence;
using PeopleHub.Application.Common.Interfaces.Services;
using PeopleHub.Application.Users.Dtos;
using PeopleHub.Application.Users.Interfaces;
using PeopleHub.Domain.Aggregates.User;
using PeopleHub.Domain.Enums;
using PeopleHub.Domain.ValueObjects;

namespace PeopleHub.Application.Users.Services;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<RegisterUserResponse> RegisterAsync(
        RegisterUserRequest request,
        CancellationToken cancellationToken = default)
    {
        if (await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
        {
            throw new InvalidOperationException("Email already exists.");
        }

        if (await _userRepository.ExistsByPhoneNumberAsync(request.PhoneNumber, cancellationToken))
        {
            throw new InvalidOperationException("Phone number already exists.");
        }

        var email = Email.Create(request.Email);
        var phoneNumber = PhoneNumber.Create(request.PhoneNumber);

        var passwordHash = _passwordHasher.HashPassword(request.Password);

        var user = new User(
            request.FirstName,
            request.LastName,
            email,
            phoneNumber,
            passwordHash);

        user.AddRole(UserRoleType.Customer);

        await _userRepository.AddAsync(user, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new RegisterUserResponse
{
    UserId = user.Id,
    Message = "Registration completed successfully."
};
    }

    public Task<LoginResponse> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto?> GetByIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}