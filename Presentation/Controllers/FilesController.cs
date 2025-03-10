using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using System.Text;
using Application.Files.Commands;
using Domain.Common;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class FilesController(ISender sender) : ControllerBase
{
    [HttpPost("upload")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        if (file.ContentType != "text/plain")
        {
            return BadRequest("Only text files are allowed.");
        }

        try
        {
            Result<FileData> result = (Result<FileData>)await sender.Send(new UploadFileCommand(file));

            return File(result!.Value.FileBytes, result.Value.Format, result.Value.FileName);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }
}
