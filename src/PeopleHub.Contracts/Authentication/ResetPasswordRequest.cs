namespace PeopleHub.Contracts.Authentication;

public sealed class ResetPasswordRequest
{
    public Guid UserId { get; init; }

    public string Otp { get; init; } = string.Empty;

    public string NewPassword { get; init; } = string.Empty;

    public string ConfirmPassword { get; init; } = string.Empty;
}