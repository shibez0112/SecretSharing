using SecretSharing.Core.Entities;

namespace SecretSharing.Core.Interfaces
{
    public interface ITextService
    {
        Task<String> UploadText(string userId, string content, bool isAutoDeleted);
        Task<IReadOnlyList<UserText>> GetTextsForUserAsync(string userId);
        Task<bool> DeleteTextAsync(string fileId);
        Task<string> AccessTextAsync(string fileId);
    }
}
