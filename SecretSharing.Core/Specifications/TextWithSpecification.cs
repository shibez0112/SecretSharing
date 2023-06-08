using SecretSharing.Core.Entities;

namespace SecretSharing.Core.Specifications
{
    public class TextWithSpecification : BaseSpecification<UserText>
    {
        // find text with userid
        public TextWithSpecification(string Id)
: base(x => x.AppUserId == Id)
        {

        }
    }
}
