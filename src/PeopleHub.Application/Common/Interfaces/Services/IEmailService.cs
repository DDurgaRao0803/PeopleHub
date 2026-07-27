namespace PeopleHub.Application.Common.Interfaces.Services;

public interface IEmailService
{
    Task SendEmailAsync(
        string toEmail,
        string subject,
        string htmlBody,
        CancellationToken cancellationToken = default);
}