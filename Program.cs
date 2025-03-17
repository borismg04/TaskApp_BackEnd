using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor(); // Agrega esta línea

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAuthService>(provider =>
{
    var context = provider.GetRequiredService<DBManagement>();
    var configuration = provider.GetRequiredService<IConfiguration>();
    var secretKey = configuration["JwtSettings:SecretKey"];
    return new AuthService(context, secretKey!);
});

builder.Services.AddScoped<IUsersService>(provider =>
{
    var context = provider.GetRequiredService<DBManagement>();
    var configuration = provider.GetRequiredService<IConfiguration>();
    var secretKey = configuration["JwtSettings:SecretKey"];
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
    var authService = provider.GetRequiredService<IAuthService>();
    return new UsersService(context, secretKey!, httpContextAccessor, authService);
});

builder.Services.AddScoped<ITaskService>(provider =>
{
    var context = provider.GetRequiredService<DBManagement>();
    var configuration = provider.GetRequiredService<IConfiguration>();
    var secretKey = configuration["JwtSettings:SecretKey"];
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
    var authService = provider.GetRequiredService<IAuthService>();
    return new TaskService(context, secretKey!, httpContextAccessor, authService);
});

builder.Services.AddDbContext<DBManagement>(options =>
    options.UseInMemoryDatabase("TaskAppDB"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();