using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecretSharing.Core.Entities;
using SecretSharing.Core.Interfaces;
using SecretSharing.Dtos;
using SecretSharing.Errors;
using SecretSharing.Extensions;

namespace SecretSharing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : BaseApiController
    {
        public ITextService TextService;
        private readonly IMapper _mapper;


        public TextController(ITextService textService, IMapper mapper)
        {
            TextService = textService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost(nameof(UploadText))]
        public async Task<ActionResult<string>> UploadText(string content, bool isAutoDeleted)
        {
            // Get user's id from claim
            var userId = HttpContext.User.RetrieveIdFromPrincipal();
            var uploadedText = await TextService.UploadText(userId, content, isAutoDeleted);
            if (uploadedText == null)
            {
                return BadRequest(new APIResponse(400, "Something went Wrong"));
            }
            return Ok(uploadedText);

        }

        [Authorize]
        [HttpGet(nameof(ListUserText))]
        public async Task<ActionResult<UserFileDto>> ListUserText()
        {
            // Get user's id from claim
            var userId = HttpContext.User.RetrieveIdFromPrincipal();
            var texts = await TextService.GetTextsForUserAsync(userId);
            var resultTexts = _mapper.Map<IReadOnlyList<UserText>, IReadOnlyList<UserTextDto>>(texts);
            return Ok(resultTexts);

        }

        [Authorize]
        [HttpDelete(nameof(DeleteUserText))]
        public async Task<ActionResult<bool>> DeleteUserText(string textId)
        {
            var deleteText = await TextService.DeleteTextAsync(textId);
            if (deleteText == false)
            {
                return BadRequest(new APIResponse(400, "Something went Wrong"));
            }
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet(nameof(AccessUserText))]
        public async Task<ActionResult<string>> AccessUserText(string textId)
        {
            var text = await TextService.AccessTextAsync(textId);
            if (text == null)
            {
                return BadRequest(new APIResponse(400, "Something went Wrong"));
            }
            return Ok(text);
        }
    }
}
