using Microsoft.AspNetCore.Diagnostics;
using Musicians.Domain.Exceptions;
using Musicians.Domain.Exceptions.AlreadyExistsException;
using Musicians.Domain.Exceptions.BadRequestException;
using Musicians.Domain.Exceptions.NotFoundException;
using System.Text.Json;

namespace Musicians.API.Middlewares
{
    public static class GlobalExceptionHandlerMiddleware
    {
        public static void ConfigureExceptionHandler(this WebApplication app,
            ILogger logger)
        {
            app.UseExceptionHandler(apiError =>
            {
                apiError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            BadRequestException => StatusCodes.Status400BadRequest,
                            CustomValidationException => StatusCodes.Status422UnprocessableEntity,
                            NotAuthenticatedException => StatusCodes.Status401Unauthorized,
                            AlreadyExistsException => StatusCodes.Status409Conflict,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        logger.LogError("Something went wrong: {Error}", contextFeature.Error);

                        if (contextFeature.Error is CustomValidationException exception)
                        {
                            await context.Response
                            .WriteAsync(JsonSerializer.Serialize(new { exception.Errors }));
                        }
                        else
                        {
                            await context.Response.WriteAsync(new ErrorDetails()
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = contextFeature.Error.Message
                            }.ToString());
                        }
                    }
                });
            });
        }

    }
}
