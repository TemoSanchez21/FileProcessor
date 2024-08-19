
using FileProcessor.Domain.Services;
using System.Net;
using FluentFTP;

namespace FileProcessor.Infrastructure.Services;

public class FluentFTPTransferService : IFileTransferService
{
    private static int _ftpPort = 21;
    private static string _ftpUrl = "192.168.1.78";
    private static string _ftpUsername = "ProcessorFile";
    private static string _ftpPassword = "Dona21";


    public async Task<bool> TransferFile(Stream fileStream, string fileName)
    {
        using var client = new FtpClient(_ftpUrl, _ftpPort);
        client.Credentials = new NetworkCredential(_ftpUsername, _ftpPassword);
        client.AutoConnect();
        var fullUrl = $"/{fileName}";
        
        var status = client.UploadStream(fileStream, fullUrl, createRemoteDir: true);

        client.AutoDispose();
        return await Task.FromResult(status == FtpStatus.Success);
    }
}
