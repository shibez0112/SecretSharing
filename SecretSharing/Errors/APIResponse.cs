namespace SecretSharing.Errors
{
    public class APIResponse
    {
        public int statusCode { get; set; }
        public string message { get; set; }

        public APIResponse(int _statusCode, string _message = null) {
            statusCode= _statusCode;
            message= _message ?? DefaultStatusCodeMessage(_statusCode);
        }
        private string DefaultStatusCodeMessage(int StatusCode)
        {
            return StatusCode switch
            {
                400 => "A bad request you have made",
                401 => "Authorized you have not",
                404 => "Resource Found it was not",
                500 => "Errors are the path to the dark side.  Errors lead to anger.   Anger leads to hate.  Hate leads to career change.",
                0 => "Some Thing Went Wrong",
                _ => throw new NotImplementedException()
            };
        }
    }
}
