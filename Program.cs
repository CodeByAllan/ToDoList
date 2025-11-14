using ToDoList.Data;
using Microsoft.EntityFrameworkCore;
using ToDoList.interfaces;
using ToDoList.Repositories;
using ToDoList.Services;
using ToDoList.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);
// Configurations
builder.Services.Configure<JwtConfigs>(builder.Configuration.GetSection("JwtConfigs"));
var jwtConfigs = builder.Configuration.GetSection("JwtConfigs").Get<JwtConfigs>();

if (jwtConfigs == null || string.IsNullOrEmpty(jwtConfigs.Secret))
{
    throw new InvalidOperationException("JwtConfigs not configured correctly or secret key missing.");
}

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigs.Secret)),
            ValidateIssuer = true,
            ValidIssuer = jwtConfigs.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtConfigs.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

// Repositories
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
// Services
builder.Services.AddScoped<ITodoItemService, TodoItemService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
