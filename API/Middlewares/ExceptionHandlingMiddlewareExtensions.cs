using System.Diagnostics.CodeAnalysis;

namespace API.Middlewares;

[ExcludeFromCodeCoverage]
public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}