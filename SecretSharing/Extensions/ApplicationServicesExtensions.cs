using Microsoft.AspNetCore.Mvc;
using SecretSharing.Application.CustomServices;
using SecretSharing.Core.Interfaces;
using SecretSharing.Errors;
using SecretSharing.Infrastructure.Data;
using SecretSharing.Infrastructure.Repositories;

namespace SecretSharing.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<StoreContext, StoreContext>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ITextService, TextService>();
            services.AddScoped<ICloudinaryServices, CloudinaryServices>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
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
