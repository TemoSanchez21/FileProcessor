namespace FileProcessor.Domain.Services;

public interface INotificationEmailService
{
    public Task<bool> SendNotificationAsync(string email);
}
