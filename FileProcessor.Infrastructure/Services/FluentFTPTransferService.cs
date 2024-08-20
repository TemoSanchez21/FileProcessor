
using FileProcessor.Domain.Services;
using System.Net;
using FluentFTP;

namespace FileProcessor.Infrastructure.Services;

public class FluentFTPTransferService : IFileTransferService
{
    private readonly int _ftpPort = 21;
    private readonly string _ftpUrl = "192.168.1.78";
    private readonly string _ftpUsername = "ProcessorFile";
    private readonly string _ftpPassword = "Dona21";

    //public FluentFTPTransferService()
    //{
    //    var prueba = Environment.GetEnvironmentVariable("FtpSettings:FtpPort");
    //    _ftpUrl = Environment.GetEnvironmentVariable("FtpSettings:FtpUrl") ?? string.Empty;
    //    _ftpPort = int.Parse( Environment.GetEnvironmentVariable("FtpSettings:FtpPort") ?? string.Empty );
    //    _ftpUsername = Environment.GetEnvironmentVariable("FtpSettings:FtpUsername") ?? string.Empty;
    //    _ftpPassword = Environment.GetEnvironmentVariable("FtpSettings:FtpPassword") ?? string.Empty;
    //}

    public async Task<bool> TransferFileAsync(Stream fileStream, string fileName)
    {
        using var client = new FtpClient(_ftpUrl, _ftpPort);
        client.Credentials = new NetworkCredential(_ftpUsername, _ftpPassword);
        client.AutoConnect();
        var fullUrl = $"/{fileName}";
        
        var status = client.UploadStream(fileStream, fullUrl, createRemoteDir: true);

        client.AutoDispose();
        return await Task.FromResult(status == FtpStatus.Success);
    }

    public async Task<Stream> DownloadFileAsync(string remotePath)
    {
        using var client = new FtpClient(_ftpUrl, _ftpPort);
        client.Credentials = new NetworkCredential(_ftpUsername, _ftpPassword);
        client.AutoConnect();

        var memoryStream = new MemoryStream();
        var status = client.DownloadStream(memoryStream, remotePath);
        memoryStream.Position = 0;

        return await Task.FromResult(memoryStream);
    }
}
