using MediatR;

namespace FileProcessor.Application.Areas.Notification.Commands.SendEmailNotification;

public class SendEmailNotificationCommand : IRequest<bool>
{
    public string Email { get; set; } = string.Empty;
}
