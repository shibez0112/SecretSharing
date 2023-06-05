using Microsoft.EntityFrameworkCore;
using SecretSharing.ExceptionMiddleware;
using SecretSharing.Extensions;
using SecretSharing.Infrastructure.Data;
using SecretSharing.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<StoreContext>(opts =>
{
    opts.UseSqlServer(
    builder.Configuration["ConnectionStrings:DefaultConnection"]);

});
builder.Services.AddDbContext<IdentityContext>(opts =>
{
    opts.UseSqlServer(
    builder.Configuration["ConnectionStrings:DefaultIdentityConnection"]);
});
builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");

app.UseMiddleware<ExceptionMiddle>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

// For intergration testing
public partial class Program { }