using SecretSharing.Errors;
using System.Net;
using System.Text.Json;

namespace SecretSharing.ExceptionMiddleware
{
    public class ExceptionMiddle
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddle> _logger;
        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionMiddle(
            RequestDelegate next,
            ILogger<ExceptionMiddle> logger,
            IHostEnvironment hostEnvironment)
        {
            _next = next;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // Set header
                _logger.LogError(ex, ex.Message);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // Check if application is still in dev
                var response = _hostEnvironment.IsDevelopment() ?
                    new APIException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new APIException((int)HttpStatusCode.InternalServerError);

                // Create new error as json and send
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);

                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}
