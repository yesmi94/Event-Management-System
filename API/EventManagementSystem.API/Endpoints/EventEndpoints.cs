// <copyright file="EventEndpoints.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.API.Endpoints
{
    using EventManagementSystem.API.Extensions;
    using EventManagementSystem.Application.DTOs.EventDtos;
    using EventManagementSystem.Application.DTOs.RegistrationDtos;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Application.UseCases.EventRegistrations.GetUserRegistrationsByEvent;
    using EventManagementSystem.Application.UseCases.Events.CreateEvent;
    using EventManagementSystem.Application.UseCases.Events.DeleteEvent;
    using EventManagementSystem.Application.UseCases.Events.GetEventImage;
    using EventManagementSystem.Application.UseCases.Events.GetEvents;
    using EventManagementSystem.Application.UseCases.Events.UpdateEvent;
    using EventManagementSystem.Application.UseCases.Events.UploadEventImage;
    using MediatR;
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class EventEndpoints : IEndpointGroup
    {
        private readonly ILogger<EventEndpoints> logger;

        public EventEndpoints(ILogger<EventEndpoints> logger)
        {
            this.logger = logger;
        }

        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/events");

            group.MapPost(
                "/",
                [Authorize(Roles = "Admin")]
                async (IMediator mediator, [FromBody] CreateEventCommand command) =>
            {
                var result = await mediator.Send(command);

                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse([result.Error!], "Couldn't create the new event");
                    this.logger.LogWarning("Failed: Failed to create the new event. Error: {Error}", result.Error);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<GetEventDto>.SuccessResponse(result.Value!, $"Event - {result.Value!.Title} added successfully");
                this.logger.LogInformation("Event - {Event Name} added successfully", result.Value.Title);
                return Results.Ok(successResponse);
            });

            group.MapGet(
                "/",
                [Authorize(Roles = "Admin")]
                async (IMediator mediator) =>
            {
                var query = new GetEventsQuery();

                var result = await mediator.Send(query);
                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse(new List<string> { result.Error! }, $"Couldn't get the events list: {result.Error}");
                    this.logger.LogWarning("Failed: Couldn't get the events list. Error: {Error}", result.Error);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<List<GetEventDto>>.SuccessResponse(result.Value!, "Events list retrieved successfully");
                this.logger.LogInformation("Events list retrieved successfully.");
                return Results.Ok(successResponse);
            });

            group.MapPut(
                "/{eventId}",
                [Authorize(Roles = "Admin")]
                async (IMediator mediator, [FromBody] UpdateEventCommand command, [FromRoute] string eventId) =>
            {
                var result = await mediator.Send(command);

                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse([result.Error!], "Couldn't do the updates for the event");
                    this.logger.LogWarning("Failed: Couldn't do the updates for the event. Error: {Error}", result.Error);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<GetEventDto>.SuccessResponse(result.Value!, $"Event deatils for - {result.Value!.Title} was updated successfully");
                this.logger.LogInformation("Event details for - {Event Name} was updated successfully", result.Value.Title);
                return Results.Ok(successResponse);
            });

            group.MapDelete(
                "/{eventId}",
                [Authorize(Roles = "Admin")]
                async (IMediator mediator, [FromRoute] string eventId) =>
            {
                var command = new DeleteEventCommand(eventId);

                var result = await mediator.Send(command);

                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse(new List<string> { result.Error! }, $"Couldn't delete the event: {result.Error}");
                    this.logger.LogWarning("Failed: Couldn't delete the event. Error: {Error}", result.Error);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<string>.SuccessResponse(result.Value!, result.Value);
                this.logger.LogInformation("Event deletion: {Success Message}", result.Value);
                return Results.Ok(successResponse);
            });

            group.MapPost(
                "/{eventId}/upload-image",
                [Authorize(Roles = "Admin")]
                async (IMediator mediator, string eventId, IFormFile file) =>
            {
                var command = new UploadEventImageCommand(eventId, file);
                var result = await mediator.Send(command);

                if (!result.IsSuccess)
                {
                    var respose = Result<string>.Failure($"Falied to upload the image for the event: {result.Error}");
                    this.logger.LogWarning("Failed:Couldn't uplaod the image for the event. Error: {Error}", result.Error);
                    return Results.BadRequest(respose);
                }

                var successResponse = Response<string>.SuccessResponse(result.Value!);
                this.logger.LogInformation("Successfully uploaded the event image: {Success Message}", result.Value);
                return Results.Ok(successResponse);
            }).DisableAntiforgery();

            group.MapGet(
                "/{eventId}/registrations",
                [Authorize(Roles = "Admin")]
                async (IMediator mediator, [FromRoute] string eventId) =>
                {
                    var query = new GetRegistrationsByEventQuery(eventId);

                    var result = await mediator.Send(query);
                    if (!result.IsSuccess)
                    {
                        var response = Response<string>.FailureResponse(new List<string> { result.Error! }, $"Couldn't get the registrations list: {result.Error}");
                        this.logger.LogWarning("Failed: Couldn't get the registrations list. Error: {Error}", result.Error);
                        return Results.BadRequest(response);
                    }

                    var successResponse = Response<List<GetRegistrationDto>>.SuccessResponse(result.Value!, "Registrations retrieved successfully");
                    this.logger.LogInformation("Registrations retrieved successfully.");
                    return Results.Ok(successResponse);
                });

            group.MapGet("/{eventId}/event-image", async (IMediator mediator, string eventId, HttpRequest request) =>
            {
                var query = new GetEventImageQuery(eventId);

                var result = await mediator.Send(query);
                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse(new List<string> { result.Error! }, $"Couldn't get the image for the event: {result.Error}");
                    this.logger.LogWarning("Failed: Couldn't get the image for the event. Error: {Error}", result.Error);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<string>.SuccessResponse(result.Value!, "Image retrieved successfully");
                this.logger.LogInformation("Image retrieved successfully.");
                return Results.Ok(successResponse);
            });
        }
    }
}
