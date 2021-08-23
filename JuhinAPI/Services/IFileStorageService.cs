using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Services
{
    public interface IFileStorageService
    {
        Task<string> EditFile(byte[] content, string extension, string containerName, string fileRoute, string contentType);
        Task<string> DeleteFile(string fileRoute, string containerName);
        Task<string> SaveFile(byte[] content, string extension, string containerName, string contentType);
    }
}
