using MediatR;

namespace FileProcessor.Application.Areas.File.Queries.GetFileById;

public class GetFileByIdQuery : IRequest<(Stream, string)>
{
    public Guid Id { get; set; }
}
