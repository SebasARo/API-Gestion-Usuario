
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace UserManagementAPI.Middleware;

//Manejo de errores globales
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    //Permite escribir mensajes en los logs.
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Continúa con el siguiente middleware
            await _next(context);
        }
        catch (Exception ex)
        {
            // Registra el error en la consola
            _logger.LogError(ex,
                "Ocurrió un error inesperado mientras se procesaba la solicitud."); 

            // Configura la respuesta HTTP
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            // Objeto que será convertido automáticamente a JSON
            var error = new
            {
                message = "Error interno del servidor.",
                statusCode = 500
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(error));
        }
    }
}