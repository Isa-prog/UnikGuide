using Destinationosh.Models;
using MatBlazor;
using Microsoft.EntityFrameworkCore;

namespace Destinationosh.Services;

public class FileService : IFileService
{
    private readonly ApplicationContext _db;
    private readonly IFileUploaderService _fileUploaderService;
    public FileService(ApplicationContext db, IFileUploaderService fileUploaderService)
    {
        _db = db;
        _fileUploaderService = fileUploaderService;
    }

    public async Task UploadFile(IMatFileUploadEntry file)
    {
        using (var fileStream = await _fileUploaderService.PrepareStream(file.Name, "imgs"))
        {
            await file.WriteToStreamAsync(fileStream);
        }
    }

    public async Task<bool> IsFileExists(string filePath)
    {
        return await _db.Files.AnyAsync(f => f.Path == filePath);
    }

    public async Task<FileModel[]> GetFiles()
    {
        return await _db.Files.ToArrayAsync();
    }

    public async Task<FileModel[]> GetFiles(int page, int pageSize)
    {
        return await _db.Files.Skip((page) * pageSize).Take(pageSize).ToArrayAsync();
    }

    public async Task<int> GetFilesCount()
    {
        return await _db.Files.CountAsync();
    }

    public async Task<FileModel[]> GetImages()
    {
        return await _db.Files.Where(file => file.Path.Contains("imgs")).ToArrayAsync();
    }

    public async Task<FileModel[]> GetImages(int page, int pageSize)
    {
        return await _db.Files.Where(file => file.Path.Contains("imgs")).Skip((page) * pageSize).Take(pageSize).ToArrayAsync();
    }

    public async Task<int> GetImagesCount()
    {
        return await _db.Files.CountAsync(file => file.Path.Contains("imgs"));
    }
}