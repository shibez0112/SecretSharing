using SecretSharing.Core.Dtos;

namespace SecretSharing.Application.CustomServices
{
    public interface IFileService
    {
        Task<String> UploadFile(string userId, FileDto fileDto, bool isAutoDeleted);
    }
}