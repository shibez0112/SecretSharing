using SecretSharing.Core.Dtos;
using SecretSharing.Core.Entities;

namespace SecretSharing.Application.CustomServices
{
    public interface IFileService
    {
        Task<String> UploadFile(string userId, FileDto fileDto, bool isAutoDeleted);
        Task<IReadOnlyList<UserFile>> GetFilesForUserAsync(string userId);
        Task<bool> DeleteFileAsync(string fileId);
    }
}