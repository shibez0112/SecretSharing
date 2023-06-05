using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecretSharing.Application.CustomServices;
using SecretSharing.Core.Dtos;
using SecretSharing.Core.Entities;
using SecretSharing.Dtos;
using SecretSharing.Errors;
using SecretSharing.Extensions;

namespace SecretSharing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : BaseApiController
    {
        public IFileService FileService;
        private readonly IMapper _mapper;


        public FileController(IFileService fileService, IMapper mapper)
        {
            FileService = fileService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost(nameof(UploadFile))]
        public async Task<ActionResult<string>> UploadFile(IFormFile file, bool isAutoDeleted)
        {
            var userId = HttpContext.User.RetrieveIdFromPrincipal();
            var fileDto = new FileDto { File = file };
            var uploadedFile = await FileService.UploadFile(userId, fileDto, isAutoDeleted);
            if (uploadedFile == null)
            {
                return BadRequest(new APIResponse(400, "Something went Wrong"));
            }
            return Ok(uploadedFile);

        }

        [Authorize]
        [HttpGet(nameof(ListUserFile))]
        public async Task<ActionResult<UserFileDto>> ListUserFile()
        {
            var userId = HttpContext.User.RetrieveIdFromPrincipal();
            var files = await FileService.GetFilesForUserAsync(userId);
            var resultFile = _mapper.Map<IReadOnlyList<UserFile>, IReadOnlyList<UserFileDto>>(files);
            return Ok(resultFile);

        }

        [Authorize]
        [HttpDelete(nameof(DeleteUserFile))]
        public async Task<ActionResult<bool>> DeleteUserFile(string fileId)
        {
            var deleteFile = await FileService.DeleteFileAsync(fileId);
            if (deleteFile == false)
            {
                return BadRequest(new APIResponse(400, "Something went Wrong"));
            }
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet(nameof(AccessUserFile))]
        public async Task<ActionResult<string>> AccessUserFile(string fileId)
        {
            var file = await FileService.AccessFileAsync(fileId);
            if (file == null)
            {
                return BadRequest(new APIResponse(400, "Something went Wrong"));
            }
            return Ok(file);
        }
    }
}
