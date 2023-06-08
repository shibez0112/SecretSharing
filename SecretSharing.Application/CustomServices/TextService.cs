using SecretSharing.Core.Entities;
using SecretSharing.Core.Interfaces;
using SecretSharing.Core.Specifications;

namespace SecretSharing.Application.CustomServices
{
    public class TextService : ITextService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TextService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> AccessTextAsync(string textId)
        {
            var result = await _unitOfWork.repository<UserText>().GetByIdAsync(textId);
            if (result != null)
            {
                var content = result.Content;
                // If text is mark as deleted after access
                if (result.IsAutoDeleted)
                {
                    await _unitOfWork.repository<UserText>().DeleteByIdAsync(textId);
                    await _unitOfWork.Complete();
                }
                return content;
            }
            return null;
        }

        public async Task<bool> DeleteTextAsync(string textId)
        {
            await _unitOfWork.repository<UserText>().DeleteByIdAsync(textId);
            var result = await _unitOfWork.Complete();
            if (result <= 0)
            {
                return false;
            }

            return true;
        }

        public async Task<IReadOnlyList<UserText>> GetTextsForUserAsync(string userId)
        {
            var spec = new TextWithSpecification(userId);
            return await _unitOfWork.repository<UserText>().ListAsync(spec);
        }

        public async Task<string> UploadText(string userId, string content, bool isAutoDeleted)
        {
            var text = new UserText { Content = content, IsAutoDeleted = isAutoDeleted, AppUserId = userId };
            if (text != null)
            {
                _unitOfWork.repository<UserText>().Add(text);
            }
            var result = await _unitOfWork.Complete();
            if (result <= 0)
            {
                return null;
            }

            return text.Id.ToString();
        }
    }
}
