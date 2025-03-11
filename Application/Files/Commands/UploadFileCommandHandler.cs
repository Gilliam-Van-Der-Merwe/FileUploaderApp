using Application.Abstractions.Messaging;
using Application.Files.Cache;
using Domain.Common;

namespace Application.Files.Commands;

public class UploadFileCommandHandler(IFileCacheService fileCacheService) : ICommandHandler<UploadFileCommand>
{
    /// <summary>
    /// Handles the the file uploaded via swagger API and caches it after it has been mutated.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result/*<FileInfo>*/> Handle(UploadFileCommand command, CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(command.File.OpenReadStream());

        string fileContent = await reader.ReadToEndAsync(cancellationToken);

        string editedContent = fileContent + $"\nEdited by API on {DateTime.UtcNow} with uniqueId: {Guid.NewGuid()}";

        fileCacheService.Set(command.File.FileName, editedContent);

        return Result.Success();
    }
}
