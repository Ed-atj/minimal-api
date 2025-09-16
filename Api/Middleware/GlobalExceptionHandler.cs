using Lombok.NET;
using MinimalApi.Exceptions.Base;
using MinimalApi.Exceptions.Custom.Password;

namespace MinimalApi.Middleware;

[RequiredArgsConstructor]
public partial class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;
    

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Um erro aconteceu durante o request.");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            switch (exception)
            {
                case BaseNotFoundException baseNotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsJsonAsync(new { message = baseNotFoundException.Message });
                    break;
                case BaseAlreadyExistsException baseAlreadyExistsException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    await context.Response.WriteAsJsonAsync(new { message = baseAlreadyExistsException.Message });
                    break;
                case PasswordInvalidHashException passwordInvalidHashException:
                    context.Response.StatusCode = StatusCodes.Status412PreconditionFailed;
                    await context.Response.WriteAsJsonAsync(new { message = passwordInvalidHashException.Message });
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsJsonAsync(new { message = "Erro interno do servidor." });
                    break;
            }
        }
    }
    
}