using Microsoft.AspNetCore.Mvc;
using SecretSharing.Application.CustomServices;
using SecretSharing.Core.Interfaces;
using SecretSharing.Errors;
using SecretSharing.Infrastructure.Data;


namespace SecretSharing.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<StoreContext, StoreContext>();
            services.AddScoped<ITokenService, TokenService>();
            services.Configure<ApiBehaviorOptions>(options => 
            options.InvalidModelStateResponseFactory = ActionContext =>
            {
                var error = ActionContext.ModelState
                            .Where(e => e.Value.Errors.Count > 0)
                            .SelectMany(e => e.Value.Errors)
                            .Select(e => e.ErrorMessage).ToArray();
                var errorresponce = new APIValidationErrorResponse
                {
                    Errors = error
                };
                return new BadRequestObjectResult(error);
            });
            return services;
        }
    }
}
