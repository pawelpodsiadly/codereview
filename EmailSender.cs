public class SmtpEmailSender : IEmailSender
{
    private readonly SmtpClient smtpClient;

    public SmtpEmailSender(string smtpServer, int smtpPort, string username, string password)
    {
        if (string.IsNullOrWhiteSpace(smtpServer)) throw new ArgumentNullException(nameof(smtpServer));
        if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException(nameof(username));
        if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));

        smtpClient = new SmtpClient(smtpServer, smtpPort)
        {
            Credentials = new NetworkCredential(username, password),
            EnableSsl = true
        };
    }

    public async Task SendEmailAsync(string from, string to, string subject, string body)
    {
        if (string.IsNullOrWhiteSpace(from)) throw new ArgumentNullException(nameof(from));
        if (string.IsNullOrWhiteSpace(to)) throw new ArgumentNullException(nameof(to));
        if (string.IsNullOrWhiteSpace(subject)) throw new ArgumentNullException(nameof(subject));
        if (string.IsNullOrWhiteSpace(body)) throw new ArgumentNullException(nameof(body));

        var mailMessage = new MailMessage(from, to, subject, body);
        await smtpClient.SendMailAsync(mailMessage);
    }
}
