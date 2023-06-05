using SecretSharing.Core.Dtos;
using SecretSharing.Core.Entities;

namespace SecretSharing.Core.Interfaces
{
    public interface ICloudinaryServices
    {
        UserFile UploadUserFile(string userId, FileDto fileDto, bool isAutoDeleted);
    }
}