using System.Numerics;
using System.Text;
using BookTrackr.Application.Interfaces.Auth;
using BookTrackr.Application.Interfaces.Services.BookTrackr;
using BookTrackr.Application.Services;
using BookTrackr.Application.Services.BookTrackr;
using BookTrackr.Application.Utils;
using BookTrackr.Domain.Exceptions;
using BookTrackr.Domain.Models.Responses.Shared;
using BookTrackr.Infrastructure.Database.Contexts;
using BookTrackr.Infrastructure.Interfaces.Repositories;
using BookTrackr.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TODOApp.Domain.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddExceptionHandler((options) =>
{
    options.ExceptionHandler = async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>();

        int statusCode = 500;
        string message = "Internal Server Error";

        if (exception is AuthException)
        {
            statusCode = 401;
            message = "Authentication Error";
        }
        else if (exception is ClientFaultException)
        {
            statusCode = 400;
            message = "Bad Request";
        }
        else if (exception is ServerFaultException)
        {
            statusCode = 500;
            message = "Internal Server Error";
        }

        context.Response.StatusCode = statusCode;

        var errorResponse = new ErrorResponse(statusCode, message, new Error());
        if (exception is CustomException)
        {
            statusCode = ((CustomException)exception).Code;
            message = ((CustomException)exception).Message;
        }

        errorResponse.Error = new Error
        {
            Code = statusCode,
            ErrorMessage = message
        };

        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        }));

    };
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:JWTSecretKey"])),
    };
});


builder.Services.AddDbContext<DatabaseContext>();

// Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBookTrackrService, BookTrackrService>();
// Repositories
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IBookTrackrRepository, BookTrackrRepository>();
// Utils
builder.Services.AddScoped<PasswordUtils>();
builder.Services.AddScoped<JWTUtils>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookTrackr.Api v1"));
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors(builder =>
{
    builder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials();
});

app.Run();
