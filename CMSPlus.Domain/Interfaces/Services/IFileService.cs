using Microsoft.AspNetCore.Http;

namespace CMSPlus.Domain.Interfaces;

public interface IFileService
{
    public Task<string> ZipFiles(IEnumerable<IFormFile> files, string destinationPath,string zipName);
}