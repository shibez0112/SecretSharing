using SecretSharing.Core.Entities;

namespace SecretSharing.Core.Specifications
{
    public class FileWithSpecification : BaseSpecification<UserFile>
    {
        // Find find by user id
        public FileWithSpecification(string Id)
        : base(x => x.AppUserId == Id)
        {

        }
    }
}
