﻿using Identity.Domain.Exceptions;
using Identity.Domain.Exceptions.BadRequestException;
using Identity.Domain.Exceptions.NotFoundException;
using Microsoft.AspNetCore.Diagnostics;

namespace Identity.API.Middlewares
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
                            _ => StatusCodes.Status500InternalServerError
                        };

                        logger.LogError("Something went wrong: {Error}", contextFeature.Error);

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }.ToString());
                    }
                });
            });
        }
    }
}
