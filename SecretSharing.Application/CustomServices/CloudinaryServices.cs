using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using SecretSharing.Core.Dtos;
using SecretSharing.Core.Entities;
using SecretSharing.Core.Interfaces;

namespace SecretSharing.Application.CustomServices
{
    public class CloudinaryServices : ICloudinaryServices
    {
        private readonly IConfiguration _configuration;
        private readonly CloudinarySettings _cloudinarySettings;
        private readonly Cloudinary _cloudinary;

        // Initialize account using Cloudinary API
        public CloudinaryServices(IConfiguration configuration)
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
            // Get file from controller
            var file = fileDto.File;

            var uploadResult = new RawUploadResult();

            // Check if it is a mutiple fille
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new RawUploadParams()
                    {
                        File = new FileDescription(file.Name, stream)
                    };
                    // Upload to cloudinary
                    uploadResult = _cloudinary.Upload(uploadParams);
                }

            }

            fileDto.Url = uploadResult.Url.ToString();
            fileDto.PublicId = uploadResult.PublicId;

            // Return result after successfully upload
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

