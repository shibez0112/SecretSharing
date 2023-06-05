using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecretSharing.Application.CustomServices;
using SecretSharing.Core.Dtos;
using SecretSharing.Core.Entities;
using SecretSharing.Errors;
using SecretSharing.Extensions;

namespace SecretSharing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : BaseApiController
    {
        public IFileService FileService;
        public FileController(IFileService fileService)
        {
            FileService = fileService;
        }

        [Authorize]
        [HttpPost(nameof(UploadFile))]
        public async Task<ActionResult<UserFile>> UploadFile(IFormFile file, bool isAutoDeleted)
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
        public async Task<ActionResult<UserFile>> ListUserFile()
        {
            var userId = HttpContext.User.RetrieveIdFromPrincipal();
            var files = await FileService.GetFilesForUserAsync(userId);
            return Ok(files);

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
    }
}
