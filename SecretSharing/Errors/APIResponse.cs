namespace SecretSharing.Errors
{
    public class APIResponse
    {
        public int statusCode { get; set; }
        public string message { get; set; }

        public APIResponse(int _statusCode, string _message = null)
        {
            statusCode = _statusCode;
            message = _message ?? DefaultStatusCodeMessage(_statusCode);
        }
        private string DefaultStatusCodeMessage(int StatusCode)
        {
            return StatusCode switch
            {
                400 => "A bad request you have made",
                401 => "You are not Authorized",
                404 => "Resource not found",
                500 => "Internal Server Error",
                0 => "SomeThing Went Wrong",
                _ => throw new NotImplementedException()
            };
        }
    }
}
