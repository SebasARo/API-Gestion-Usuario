
using System.Diagnostics;

namespace UserManagementAPI.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(
        RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Inicia el cronómetro
        var stopwatch = Stopwatch.StartNew();

        // Información de la solicitud
        _logger.LogInformation(
            "Incoming Request: {Method} {Path}",
            context.Request.Method,
            context.Request.Path);

        // Continúa con el siguiente middleware
        await _next(context);

        // Detiene el cronómetro
        stopwatch.Stop();

        // Información de la respuesta
        _logger.LogInformation(
            "Outgoing Response: {StatusCode} - {ElapsedMilliseconds} ms",
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds);
    }
}