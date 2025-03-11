using Application.Abstractions.Messaging;
using Domain.Files;
using FileInfo = Domain.Files.FileInfo;

namespace Application.Files.Queries;

public sealed record GetFileByNameQuery(string FileName) : IQuery<FileInfo>
{
}
