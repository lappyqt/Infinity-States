using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Infinity_States.Services;

public interface IFileHandling
{
    Task UploadFile(IFormFile file, string path);
    Task UploadFileWithDeletingPrevious(IFormFile file, string path, string previousFilePath);
    Task DeleteFile(string path);
}