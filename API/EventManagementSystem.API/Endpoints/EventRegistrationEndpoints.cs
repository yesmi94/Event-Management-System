// <copyright file="EventRegistrationEndpoints.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.API.Endpoints
{
    using EventManagementSystem.API.Extensions;
    using EventManagementSystem.Application.DTOs.RegistrationDtos;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Application.UseCases.EventRegistrations.CancelRegistration;
    using EventManagementSystem.Application.UseCases.EventRegistrations.CreateEventRegistration;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class EventRegistrationEndpoints : IEndpointGroup
    {
        private readonly ILogger<EventRegistrationEndpoints> logger;

        public EventRegistrationEndpoints(ILogger<EventRegistrationEndpoints> logger)
        {
            this.logger = logger;
        }

        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/registrations");

            group.MapPost(
                "/",
                [Authorize(Roles = "Public User")]
                async (IMediator mediator, [FromBody] CreateEventRegistrationCommand command) =>
            {
                var result = await mediator.Send(command);

                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse([result.Error!], "Couldn't create the new registration");
                    this.logger.LogWarning("Failed: Failed to add the new registration. Error: {Error}", result.Error);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<GetRegistrationDto>.SuccessResponse(result.Value!, $"Registration - {result.Value} added successfully");
                this.logger.LogInformation("Registration - {result.Value} added successfully", result.Value);
                return Results.Ok(successResponse);
            });

            group.MapDelete(
                "/{registrationId}",
                [Authorize(Roles = "Public User")]
                async (IMediator mediator, [FromRoute] string registrationId) =>
            {
                var command = new CancelRegistrationCommand(registrationId);

                var result = await mediator.Send(command);

                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse(new List<string> { result.Error! }, $"Couldn't cancel the registration: {result.Error}");
                    this.logger.LogWarning("Failed: Couldn't cancel the registration. Error: {Error}", result.Error);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<string>.SuccessResponse(result.Value!, result.Value);
                this.logger.LogInformation("Cancel Registration: {Success Message}", result.Value);
                return Results.Ok(successResponse);
            });
        }
    }
}
