namespace SecretSharing.Dtos
{
    public class UserFileDto
    {
        public Guid Id { get; set; }
        public string PublicId { get; set; }
        public bool IsAutoDeleted { get; set; }
    }
}
