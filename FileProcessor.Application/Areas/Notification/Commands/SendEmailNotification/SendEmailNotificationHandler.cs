using FileProcessor.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FileProcessor.Application.Areas.Notification.Commands.SendEmailNotification;

public class SendEmailNotificationHandler(
    INotificationEmailService emailService,
    ILogger<SendEmailNotificationHandler> logger
) : IRequestHandler<SendEmailNotificationCommand, bool>
{
    public async Task<bool> Handle(SendEmailNotificationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Sending notification email to {Email}", request.Email);
        var result = await emailService.SendNotificationAsync(request.Email);
        return result;
    }
}
