using System.Text.Json;

namespace UserManagementAPI.Middleware;

public class TokenAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TokenAuthenticationMiddleware> _logger;

    // Token válido para la autenticación
    private const string ValidToken = "my-secret-token";

    public TokenAuthenticationMiddleware(
        RequestDelegate next,
        ILogger<TokenAuthenticationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Permitimos acceder a Swagger sin autenticación
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            await _next(context);
            return;
        }

        // Buscamos la cabecera Authorization
        if (!context.Request.Headers.TryGetValue("Authorization", out var header))
        {
            _logger.LogWarning("la cabecera Authorization no esta presente.");

            await Unauthorized(context, "la cabecera Authorization no esta presente.");
            return;
        }

        var token = header.ToString();

        // Verificamos que tenga el formato Bearer
        if (!token.StartsWith("Bearer "))
        {
            _logger.LogWarning("la cabecera Authorization tiene un formato inválido.");

            await Unauthorized(context, "la cabecera Authorization tiene un formato inválido.");
            return;
        }

        // Extraemos únicamente el valor del token
        token = token["Bearer ".Length..].Trim();

        // Comparamos el token
        if (token != ValidToken)
        {
            _logger.LogWarning("Token inválido.");

            await Unauthorized(context, "Token invalido.");
            return;
        }

        // Todo correcto
        await _next(context);
    }

    private static async Task Unauthorized(HttpContext context, string message)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = message
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}