// <copyright file="LoggingPipeline.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.Behaviors
{
    using EventManagementSystem.Application.Patterns;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class LoggingPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingPipeline<TRequest, TResponse>> logger;

        public LoggingPipeline(ILogger<LoggingPipeline<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            this.logger.LogInformation(
                "Starting Request {@RequestName}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                DateTime.UtcNow);

            var response = await next();

            if (response is Result result && result.IsFailure)
            {
                this.logger.LogError(
                    "Request Failed {@RequestName}, {@Error}, {@DateTimeUtc}",
                    typeof(TRequest).Name,
                    result.Error,
                    DateTime.UtcNow);
            }

            this.logger.LogInformation(
                "Completed Request {@RequestName}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                DateTime.UtcNow);

            return response;
        }
    }
}
