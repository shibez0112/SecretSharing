using SecretSharing.Core.Entities;

namespace SecretSharing.Core.Specifications
{
    public class TextWithSpecification : BaseSpecification<UserText>
    {
        public TextWithSpecification(string Id)
: base(x => x.AppUserId == Id)
        {

        }
    }
}
