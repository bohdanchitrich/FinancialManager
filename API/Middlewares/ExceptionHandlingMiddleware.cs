using API.DTOs.ExceptionHandling;
using API.DTOs.Exceptions;
using API.Resources;
using Domain.Categories;
using System.Data.Entity.Core;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security.Authentication;
using System.Text.Json;

namespace API.Middlewares;

[ExcludeFromCodeCoverage]
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ArgumentNullException exception)
        {
            await HandleExceptionAsync(context, exception.Message, HttpStatusCode.BadRequest,
                en_US.ArgumentNullExceptionResponseMessage);
        }
        catch (KeyNotFoundException exception)
        {
            await HandleExceptionAsync(context, exception.Message, HttpStatusCode.NotFound,
                en_US.KeyNotFoundExceptionResponseMessage);
        }
        catch (ArgumentOutOfRangeException exception)
        {
            await HandleExceptionAsync(context, exception.Message, HttpStatusCode.BadRequest,
                en_US.ArgumentOutOfRangeExceptionResponseMessage);

        }
        catch (ObjectNotFoundException exception)
        {
            await HandleExceptionAsync(context, exception.Message, HttpStatusCode.NotFound,
                en_US.ObjectNotFoundExceptionResponseMessage);
        }
        catch (CategoryDeleteException exception)
        {
            await HandleExceptionAsync(context, exception.Message, HttpStatusCode.BadRequest,
                en_US.CategoryDeleteExceptionResponseMessage);

        }
        catch (AuthenticationException exception)
        {
            await HandleExceptionAsync(context, exception.Message, HttpStatusCode.BadRequest,
                en_US.CategoryDeleteExceptionResponseMessage);
        }
        catch (NotAuthenticationException exception)
        {
            await HandleExceptionAsync(context, exception.Message, HttpStatusCode.Unauthorized,
                en_US.NotAuthenticatedExceptionResponseMessage);
        }
        catch (NotAuthorizationException exception)
        {
            await HandleExceptionAsync(context, exception.Message, HttpStatusCode.Forbidden,
                en_US.NotAuthorizedExceptionResponseMessage);
        }
        catch (ItemNotFoundException<Category> exception)
        {
            await HandleExceptionAsync(context, exception.Message, HttpStatusCode.NotFound,
                en_US.CategoryNotFoundExceptionMessage);
        }
        catch (FinancialTypeException exception)
        {
            await HandleExceptionAsync(context, exception.Message, HttpStatusCode.BadRequest,
                en_US.FinancialTypeExceptionResponceMessage);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception.Message, HttpStatusCode.InternalServerError, en_US.ExceptionResponseMessage);
        }
    }


    private async Task HandleExceptionAsync(HttpContext httpContext, string exMsg, HttpStatusCode httpStatusCode, string message)
    {
        _logger.LogError(exMsg);

        HttpResponse httpResponse = httpContext.Response;
        httpResponse.ContentType = "application/json";
        httpResponse.StatusCode = (int)httpStatusCode;

        ErrorDto errorDto = new ErrorDto
        {
            Message = message,
            StatusCode = (int)httpStatusCode
        };
        var result = JsonSerializer.Serialize(errorDto);
        await httpResponse.WriteAsJsonAsync(result);
    }
}