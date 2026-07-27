
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpLogging;
using UserManagementAPI.Middleware;
using UserManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All;
});

builder.Services.AddSingleton<IUserService, UserService>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


var app = builder.Build();


// Configurar el pipeline de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseHttpLogging();

// Gestor de errores global
app.UseMiddleware<ErrorHandlingMiddleware>();

// Middleware de registro de solicitudes
app.UseMiddleware<RequestLoggingMiddleware>();

// Authentication
app.UseMiddleware<TokenAuthenticationMiddleware>();

app.UseAuthentication();

app.UseAuthorization();


//controladores
app.MapControllers();


app.Run();

