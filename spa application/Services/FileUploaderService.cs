

using Destinationosh.Models;
using Microsoft.EntityFrameworkCore;

namespace Destinationosh.Services;

public class FileUploaderService : IFileUploaderService
{
    private ApplicationContext db;
    private IWebHostEnvironment env;
    public FileUploaderService(ApplicationContext db, IWebHostEnvironment env) 
    {
        this.db = db;
        this.env = env;
    }

    public async Task<FileStream> PrepareStream(string name, string path)
    {
        var file = new FileModel();
        file.Name = name;
        file.Path = $"/{path}/{name}";

        db.Files.Add(file);
        await db.SaveChangesAsync();

        return new FileStream(env.WebRootPath + file.Path, FileMode.CreateNew);
    }
}