using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;
using TaskAppBackEnd.Interface;
using TaskAppBackEnd.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor(); // Agrega esta línea

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

builder.Services.AddScoped<IAuthService>(provider =>
{
    var context = provider.GetRequiredService<DBManagement>();
    var configuration = provider.GetRequiredService<IConfiguration>();
    var secretKey = configuration["JwtSettings:SecretKey"];
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
    return new AuthService(context, secretKey!, httpContextAccessor);
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

builder.Services.AddScoped<ILogin, LoginService>();

builder.Services.AddDbContext<DBManagement>(options =>
    options.UseInMemoryDatabase("TaskAppDB"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DBManagement>();
    DBManagement.Seed(context);
}

app.UseHttpsRedirection();

app.UseCors("AllowOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();