using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;

public class GmailEmailSender : IEmailSender
{
    private readonly IConfiguration _config;

    public GmailEmailSender(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendAsync(string to, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config["Email:Username"]));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart("html") { Text = body };


        using var smtp = new SmtpClient();

        //smtp.ServerCertificateValidationCallback = (s, c, h, e) => true; //Solo en local
        await smtp.ConnectAsync(_config["Email:Host"], int.Parse(_config["Email:Port"]), SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_config["Email:Username"], _config["Email:Password"]);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
