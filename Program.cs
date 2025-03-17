using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAuthService>(provider =>
{
    var context = provider.GetRequiredService<DBManagement>();
    var configuration = provider.GetRequiredService<IConfiguration>();
    var secretKey = configuration["JwtSettings:SecretKey"];
    return new AuthService(context, secretKey);
});

builder.Services.AddDbContext<DBManagement>(options =>
    options.UseInMemoryDatabase("TaskAppDB"));

builder.Services.AddScoped<IUsersService, UsersService>();

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
