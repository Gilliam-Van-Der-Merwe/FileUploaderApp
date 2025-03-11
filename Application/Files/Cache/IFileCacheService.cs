namespace Application.Files.Cache;

public interface IFileCacheService
{
    public void Set(string fileName, string fileContent);

    public string Get(string fileName);

    public void Remove(string fileName);
}
