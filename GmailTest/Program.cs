using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

var message = new MimeMessage();
message.From.Add(new MailboxAddress("PeopleHub", "peoplehub.noreply@gmail.com"));
message.To.Add(MailboxAddress.Parse("YOUR_PERSONAL_EMAIL@gmail.com"));
message.Subject = "SMTP Test";
message.Body = new TextPart("plain")
{
    Text = "Hello"
};

using var client = new SmtpClient();

await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

await client.AuthenticateAsync(
    "peoplehub.noreply@gmail.com",
    "dmrlcaasipogfmxa");

await client.SendAsync(message);

await client.DisconnectAsync(true);
