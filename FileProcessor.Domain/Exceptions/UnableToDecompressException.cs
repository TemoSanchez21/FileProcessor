namespace FileProcessor.Domain.Exceptions;

public class UnableToDecompressException(string message) : Exception(message)
{
}
