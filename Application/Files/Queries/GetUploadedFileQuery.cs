using Application.Abstractions.Messaging;

namespace Application.Files.Queries;

public sealed record GetUploadedFileQuery : IQuery<File>
{
}
