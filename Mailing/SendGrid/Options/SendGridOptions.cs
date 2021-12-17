namespace Mailing.SendGrid.Options;

public class SendGridOptions
{
    public static readonly string Mailing = "Mailing";

    public string SendGridApiKey { get; set; }
    public string FromEmail { get; set; }
}