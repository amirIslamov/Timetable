using Mailing.Abstractions;
using Mailing.SendGrid.Options;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Mailing.SendGrid;

public class SendGridEmailSender : IEmailSender
{
    protected SendGridClient _client;

    public SendGridEmailSender(IOptions<SendGridOptions> options)
    {
        Options = options.Value;
        _client = new SendGridClient(Options.SendGridApiKey);
    }

    public SendGridOptions Options { get; set; }

    public Task SendEmail(string email, string subject, string message)
    {
        var msg = new SendGridMessage
        {
            From = new EmailAddress(Options.FromEmail),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(email));
        msg.SetClickTracking(false, false);

        return _client.SendEmailAsync(msg);
    }
}