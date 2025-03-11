using Application.Abstractions.Messaging;
using Application.Files.Cache;
using Domain.Common;
using System.Text;
using FileInfo = Domain.Files.FileInfo;


namespace Application.Files.Queries;

public class GetFileByNameQueryHandler(IFileCacheService fileCacheService) : IQueryHandler<GetFileByNameQuery, FileInfo>
{
    /// <summary>
    /// Handles getting the file by file name from the cache.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result<FileInfo>> Handle(GetFileByNameQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var file = fileCacheService.Get(query.FileName);

            byte[] fileBytes = Encoding.UTF8.GetBytes(file);

            return new FileInfo(fileBytes, "text/plain", "edited_" + query.FileName);
        }
        catch
        {
            throw;
        }
    }
}
