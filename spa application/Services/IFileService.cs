using Destinationosh.Models;
using MatBlazor;

namespace Destinationosh.Services;

public interface IFileService
{
    Task UploadFile(IMatFileUploadEntry file);
    Task<bool> IsFileExists(string filePath);
    Task<FileModel[]> GetFiles();
    Task<FileModel[]> GetFiles(int page, int pageSize);
    Task<FileModel[]> GetImages();
    Task<FileModel[]> GetImages(int page, int pageSize);
    Task<int> GetFilesCount();
    Task<int> GetImagesCount();
}