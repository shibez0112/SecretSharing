namespace SecretSharing.Errors
{
    public class APIException: APIResponse
    {
        public string Details;
        public APIException(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            details = details;
        }
    }
}
