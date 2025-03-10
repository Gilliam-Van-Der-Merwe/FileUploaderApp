using Application.Abstractions.Messaging;
using Domain.Common;
using System.Text;

namespace Application.Files.Commands;

public class UploadFileCommandHandler : ICommandHandler<UploadFileCommand, FileData>
{
    public async Task<Result<FileData>> Handle(UploadFileCommand command, CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(command.File.OpenReadStream());

        string fileContent = await reader.ReadToEndAsync();
        string editedContent = fileContent + $"\nEdited by API on {DateTime.UtcNow} with uniqueId: {Guid.NewGuid()}";

        byte[] fileBytes = Encoding.UTF8.GetBytes(editedContent);

        return new FileData(fileBytes, "text/plain", "edited_" + command.File.FileName);
    }
}
