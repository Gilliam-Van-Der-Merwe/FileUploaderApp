using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Http;
using Application.Files.Commands;
using Domain.Common;
using FileInfo = Domain.Files.FileInfo;
using Application.Files.Queries;


namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class FilesController(ISender sender) : ControllerBase
{
    /// <summary>
    /// API to allow users to upload a text file through swagger and mutate the
    /// file with the date and time of upload and uniqueId of the uploaded file.
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
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
            await sender.Send(new UploadFileCommand(file));

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// API to allow users to retrieve uploaded file that has been mutated.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    [HttpGet("file")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetFileByName(string fileName)
    {
        try
        {
            Result<FileInfo> result = (Result<FileInfo>)await sender.Send(new GetFileByNameQuery(fileName));

            return File(result!.Value.FileBytes, result.Value.Format, result.Value.FileName);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }
}
