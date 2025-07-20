// <copyright file="ValidationPipeline.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.Behaviors
{
    using EventManagementSystem.Application.Patterns;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;
        private readonly ILogger<ValidationPipeline<TRequest, TResponse>> logger;

        public ValidationPipeline(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationPipeline<TRequest, TResponse>> logger)
        {
            this.validators = validators;
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!this.validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                this.validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f is not null)
                .ToList();

            if (failures.Any())
            {
                var errorMessage = failures.Select(f => f.ErrorMessage).ToList();
                this.logger.LogError(
                    "Request Failed {@RequestName}, {@Error}, {@DateTimeUtc}",
                    typeof(TRequest).Name,
                    errorMessage,
                    DateTime.UtcNow);

                var responseType = typeof(TResponse).GetGenericArguments().First();
                var result = typeof(Result<>)
                    .MakeGenericType(responseType)
                    .GetMethod("Failure", new[] { typeof(string) }) !
                    .Invoke(null, new object[] { failures });

                return (TResponse)result!;
            }

            return await next();
        }
    }
}
