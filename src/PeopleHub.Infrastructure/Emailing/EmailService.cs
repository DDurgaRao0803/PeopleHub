using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using PeopleHub.Application.Common.Interfaces.Services;

namespace PeopleHub.Infrastructure.Emailing;

public sealed class EmailService : IEmailService
{
    private readonly EmailOptions _options;
    private readonly ILogger<EmailService> _logger;

    public EmailService(
        IOptions<EmailOptions> options,
        ILogger<EmailService> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(
        string toEmail,
        string subject,
        string htmlBody,
        CancellationToken cancellationToken = default)
    {
        var message = new MimeMessage();

        message.From.Add(
            new MailboxAddress(
                _options.FromName,
                _options.FromEmail));

        message.To.Add(
            MailboxAddress.Parse(toEmail));

        message.Subject = subject;

        message.Body = new BodyBuilder
        {
            HtmlBody = htmlBody
        }.ToMessageBody();

        using var client = new SmtpClient();

       

        await client.ConnectAsync(
            _options.Host,
            _options.Port,
            SecureSocketOptions.StartTls,
            cancellationToken);

       

        

        await client.AuthenticateAsync(
            _options.Username,
            _options.Password,
            cancellationToken);

        

        await client.SendAsync(
            message,
            cancellationToken);

        

        await client.DisconnectAsync(
            true,
            cancellationToken);

        _logger.LogInformation(
            "Email sent successfully to {Email}",
            toEmail);
    }
}