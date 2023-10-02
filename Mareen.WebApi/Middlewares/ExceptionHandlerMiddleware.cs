using Mareen.Service.Exceptions;
using Mareen.WebApi.Models;

namespace Mareen.WebApi.Middlewares;

public class ExceptionHandlerMiddleware 
{
    private readonly RequestDelegate requestDelegate;

    private readonly ILogger<ExceptionHandlerMiddleware> logger;
    public ExceptionHandlerMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlerMiddleware> logger)
    {
        this.requestDelegate = requestDelegate;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await this.requestDelegate.Invoke(httpContext);
        }

        catch (AlreadyExistException ex)
        {
            httpContext.Response.StatusCode = 403;
            await httpContext.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ex.Message
            });
        }

        catch (NotFoundException ex)
        {
            httpContext.Response.StatusCode = 404;
            await httpContext.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ex.Message
            });
        }

        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            httpContext.Response.StatusCode = 500;
            await httpContext.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ex.Message
            });
        }
    }
}
