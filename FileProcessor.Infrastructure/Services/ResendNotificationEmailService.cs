
using FileProcessor.Domain.Services;
using Resend;

namespace FileProcessor.Infrastructure.Services;

public class ResendNotificationEmailService(IResend resend) : INotificationEmailService
{
    private static string htmlTemplatePath = "../FileProcessor.Domain/Templates/Email/DocumentReviewEmail.html";

    public async Task<bool> SendNotificationAsync(string email)
    {
        var message = new EmailMessage();
        message.From = "Revision De Documentos <revision@anaydeli.com>";
        message.To.Add(email);
        message.Subject = "Notificacion de revision de documentos";
        message.HtmlBody = File.ReadAllText(htmlTemplatePath);

        var emailIdentifier = await resend.EmailSendAsync(message);

        return emailIdentifier.Success;
    }
}
