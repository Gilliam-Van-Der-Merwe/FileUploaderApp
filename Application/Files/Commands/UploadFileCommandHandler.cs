using Application.Abstractions.Messaging;
using Shared;

namespace Application.Files.Commands;

public class UploadFileCommandHandler : ICommandHandler<UploadFileCommand>
{
    public Task<Result> Handle(UploadFileCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
