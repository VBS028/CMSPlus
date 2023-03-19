using System.IO.Compression;
using CMSPlus.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
    
namespace CMSPlus.Application.Services;

public class FileService : IFileService
{
    public async Task<string> ZipFiles(IEnumerable<IFormFile> files, string destinationPath, string zipName)
    {
        var tempPath = Path.Combine(destinationPath, "temp");
        if (!Directory.Exists(tempPath))
        {
            Directory.CreateDirectory(tempPath);
        }

        foreach (var file in files)
        {
            var filename = Path.GetFileName(file.FileName);
            using (FileStream stream = new FileStream(Path.Combine(tempPath, filename), FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        var zipPath = Path.Combine(destinationPath, zipName);
        ZipFile.CreateFromDirectory(tempPath, zipPath);
        Directory.Delete(tempPath, true);

        return zipPath;
    }
}