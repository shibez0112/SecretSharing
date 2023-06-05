using Microsoft.AspNetCore.Http;

namespace SecretSharing.Core.Dtos
{
    public class FileDto
    {
        public IFormFile File { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
    }
}
