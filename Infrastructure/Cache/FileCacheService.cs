using Application.Files.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Cache;

public class FileCacheService(IMemoryCache cache) : IFileCacheService
{
    public void Set(string fileName, string fileContent)
    {
        cache.Set(fileName, fileContent, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        });
    }

    public string Get(string fileName)
    {
        if (cache.TryGetValue(fileName, out string fileContent))
        {
            return fileContent;
        }

        throw new ArgumentNullException("File Not Found");
    }

    public void Remove(string fileName)
    {
        cache.Remove(fileName);
    }
}