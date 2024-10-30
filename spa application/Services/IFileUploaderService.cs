using Destinationosh.Models;

namespace Destinationosh.Services;

public interface IFileUploaderService
{
    Task<FileStream> PrepareStream(string name, string path);
}