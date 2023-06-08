using SecretSharing.Core.Dtos;
using SecretSharing.Core.Entities;
using SecretSharing.Core.Interfaces;
using SecretSharing.Core.Specifications;

namespace SecretSharing.Application.CustomServices
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryServices _cloudinaryService;

        public FileService(IUnitOfWork unitOfWork, ICloudinaryServices cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<String> UploadFile(string userId, FileDto fileDto, bool isAutoDeleted)
        {
            var uploadedUserFile = _cloudinaryService.UploadUserFile(userId, fileDto, isAutoDeleted);
            if (uploadedUserFile != null)
            {
                _unitOfWork.repository<UserFile>().Add(uploadedUserFile);
            }
            var result = await _unitOfWork.Complete();
            if (result <= 0)
            {
                return null;
            }

            return uploadedUserFile.Id.ToString();
        }

        public async Task<IReadOnlyList<UserFile>> GetFilesForUserAsync(string userId)
        {
            var spec = new FileWithSpecification(userId);
            return await _unitOfWork.repository<UserFile>().ListAsync(spec);
        }

        public async Task<bool> DeleteFileAsync(string fileId)
        {
            await _unitOfWork.repository<UserFile>().DeleteByIdAsync(fileId);
            var result = await _unitOfWork.Complete();
            if (result <= 0)
            {
                return false;
            }

            return true;
        }

        public async Task<string> AccessFileAsync(string fileId)
        {
            // Check if file exists
            var result = await _unitOfWork.repository<UserFile>().GetByIdAsync(fileId);
            if (result != null)
            {
                var url = result.Url;
                // If file is mark as deleted after access
                if (result.IsAutoDeleted)
                {
                    await _unitOfWork.repository<UserFile>().DeleteByIdAsync(fileId);
                    await _unitOfWork.Complete();
                }
                return url;
            }
            return null;

        }

    }
}
