using Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace Application.Files.Commands;

public sealed record UploadFileCommand(IFormFile File) : ICommand<FileData>;

public record FileData(byte[] FileBytes, string Format, string FileName);
