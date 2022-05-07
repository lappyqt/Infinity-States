using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Infinity_States.Services;

public class FileHandling : IFileHandling
{
    public Task DeleteFile(string path)
    {
        FileInfo file = new FileInfo(path);

        if (file.Exists)
        {
            System.IO.File.Delete(path);
            file.Delete(); 
        }

        return Task.CompletedTask;
    }

    public async Task UploadFile(IFormFile file, string path)
    {
        if (file is not null) 
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }
        }
    }

    public async Task UploadFileWithDeletingPrevious(IFormFile file, string path, string previousFilePath)
    {
        await this.DeleteFile(previousFilePath);
        await this.UploadFile(file, path);
    }
} 