// <copyright file="GlobalExceptionHandlingMiddleware.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.API.Middlewares
{
    using System.Net;
    using Microsoft.AspNetCore.Mvc;

    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception exception)
            {
                this.logger.LogError("Exception Occurred: {Message}", exception.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Server Error",
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
