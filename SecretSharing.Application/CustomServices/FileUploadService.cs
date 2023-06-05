using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using SecretSharing.Core.Dtos;
using SecretSharing.Core.Entities;
using SecretSharing.Core.Interfaces;

namespace SecretSharing.Application.CustomServices
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IConfiguration _configuration;
        private readonly CloudinarySettings _cloudinarySettings;
        private readonly Cloudinary _cloudinary;

        public FileUploadService(IConfiguration configuration)
        {
            _configuration = configuration;
            // Get Cloudinary config into model using binder
            _cloudinarySettings = _configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
            // Config using Cloudinary API
            Account account = new Account(_cloudinarySettings.CloudName,
                                          _cloudinarySettings.ApiKey,
                                          _cloudinarySettings.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public UserFile UploadUserFile(string userId, FileDto fileDto, bool isAutoDeleted)
        {
            var file = fileDto.File;

            var uploadResult = new RawUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new RawUploadParams()
                    {
                        File = new FileDescription(file.Name, stream)
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }

            }

            fileDto.Url = uploadResult.Url.ToString();
            fileDto.PublicId = uploadResult.PublicId;

            var resultFile = new UserFile
            {
                Url = fileDto.Url,
                PublicId = uploadResult.PublicId,
                AppUserId = userId,
                IsAutoDeleted = isAutoDeleted,
            };

            return resultFile;
        }
    }

}
