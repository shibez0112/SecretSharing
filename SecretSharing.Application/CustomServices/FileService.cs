﻿using SecretSharing.Core.Dtos;
using SecretSharing.Core.Entities;
using SecretSharing.Core.Interfaces;

namespace SecretSharing.Application.CustomServices
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileUploadService _fileUploadService;

        public FileService(IUnitOfWork unitOfWork, IFileUploadService fileUploadService)
        {
            _unitOfWork = unitOfWork;
            _fileUploadService = fileUploadService;
        }

        public async Task<String> UploadFile(string userId, FileDto fileDto, bool isAutoDeleted)
        {
            var uploadedUserFile = _fileUploadService.UploadUserFile(userId, fileDto, isAutoDeleted);
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

    }
}
