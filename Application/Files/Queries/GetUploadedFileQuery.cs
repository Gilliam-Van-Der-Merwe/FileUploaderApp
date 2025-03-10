using Application.Abstractions.Messaging;
using Domain.Files;

namespace Application.Files.Queries;

public sealed record GetUploadedFileQuery : IQuery<UploadedFile>
{
}
