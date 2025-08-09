// <copyright file="EventEndpoints.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.API.Endpoints
{
    using System.Security.Claims;
    using EventManagementSystem.API.Extensions;
    using EventManagementSystem.Application.DTOs.EventDtos;
    using EventManagementSystem.Application.DTOs.RegistrationDtos;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Application.UseCases.EventRegistrations.GetUserRegistrationsByEvent;
    using EventManagementSystem.Application.UseCases.Events.CreateEvent;
    using EventManagementSystem.Application.UseCases.Events.DeleteEvent;
    using EventManagementSystem.Application.UseCases.Events.GetEventById;
    using EventManagementSystem.Application.UseCases.Events.GetEventImage;
    using EventManagementSystem.Application.UseCases.Events.GetEvents;
    using EventManagementSystem.Application.UseCases.Events.GetEventType;
    using EventManagementSystem.Application.UseCases.Events.GetFilteredEvents;
    using EventManagementSystem.Application.UseCases.Events.GetSearchedEvents;
    using EventManagementSystem.Application.UseCases.Events.UpdateEvent;
    using EventManagementSystem.Application.UseCases.Events.UploadEventImage;
    using MediatR;
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
                async (IMediator mediator, [FromBody] CreateEventCommand command, HttpContext httpContext) =>
            {
                var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrWhiteSpace(userId))
                {
                    this.logger.LogWarning("User ID not found in token.");
                    return Results.Unauthorized();
                }

                command.newEventDto.CreatedByUserId = userId;

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
                [Authorize(Roles = "Admin, Public User")]
                async (IMediator mediator, [FromQuery] int page, [FromQuery] int pageSize) =>
            {
                var query = new GetEventsQuery(page, pageSize);

                var result = await mediator.Send(query);

                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse(new List<string> { result.Error! }, $"Couldn't get the events list: {result.Error}");
                    this.logger.LogWarning("Failed: Couldn't get the events list. Error: {Error}", result.Error);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<PaginatedResult<GetEventDto>>.SuccessResponse(result.Value!, "Events list retrieved successfully");
                this.logger.LogInformation("Events list retrieved successfully.");
                return Results.Ok(successResponse);
            });

            group.MapGet(
                "/searched",
                [Authorize(Roles = "Admin, Public User")]
                async (IMediator mediator, [FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string searchTerm) =>
                {
                    var query = new GetSearchedEventsQuery(page, pageSize, searchTerm);

                    var result = await mediator.Send(query);
                    if (!result.IsSuccess)
                    {
                        var response = Response<string>.FailureResponse(new List<string> { result.Error! }, $"Couldn't get the search results: {result.Error}");
                        this.logger.LogWarning("Failed: Couldn't get the search results. Error: {Error}", result.Error);
                        return Results.BadRequest(response);
                    }

                    var successResponse = Response<PaginatedResult<GetEventDto>>.SuccessResponse(result.Value!, "Search Results retrieved successfully");
                    this.logger.LogInformation("Search results retrieved successfully.");
                    return Results.Ok(successResponse);
                });

            group.MapGet(
            "/filtered",
            [Authorize(Roles = "Admin, Public User")]
            async (
                [FromServices] IMediator mediator,
                [FromQuery] string? search,
                [FromQuery] string? category,
                [FromQuery] string? location,
                [FromQuery] string? status,
                [FromQuery] DateTime? dateFrom,
                [FromQuery] DateTime? dateTo,
                [FromQuery] int page,
                [FromQuery] int pageSize) =>
            {
                var query = new GetFilteredEventsQuery(page, pageSize, search, category, location, status, dateFrom, dateTo);
                var result = await mediator.Send(query);

                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse(new List<string> { result.Error! }, $"Couldn't get the filtered events: {result.Error}");
                    this.logger.LogWarning("Failed: Couldn't get the filtered events. Error: {Error}", result.Error);
                    return Results.BadRequest(Response<string>.FailureResponse([result.Error!], "Failed to get the filtered events"));
                }

                var successResponse = Response<PaginatedResult<GetEventDto>>.SuccessResponse(result.Value!, "Filtered Events list retrieved successfully");
                this.logger.LogInformation("Filtered Events retrieved successfully. {Results}", result.Value!);
                return Results.Ok(Response<PaginatedResult<GetEventDto>>.SuccessResponse(result.Value!, "Filtered Events retrieved"));
            });

            group.MapGet(
                "/{eventId}",
                [Authorize(Roles = "Admin, Public User")]
                async (IMediator mediator, [FromRoute] string eventId) =>
                {
                    var query = new GetEventByIdQuery(eventId);

                    var result = await mediator.Send(query);
                    if (!result.IsSuccess)
                    {
                        var response = Response<string>.FailureResponse(new List<string> { result.Error! }, $"Couldn't get the event: {result.Error}");
                        this.logger.LogWarning("Failed: Couldn't get the event. Error: {Error}", result.Error);
                        return Results.BadRequest(response);
                    }

                    var successResponse = Response<GetEventDto>.SuccessResponse(result.Value!, "Events list retrieved successfully");
                    this.logger.LogInformation("Event retrieved successfully.");
                    return Results.Ok(successResponse);
            });

            group.MapPut(
                "/{eventId}",
                [Authorize(Roles = "Admin")]
                async (IMediator mediator, [FromBody] UpdateEventCommand command, [FromRoute] string eventId) =>
            {
                command.updateEventDto.Id = eventId;
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
                async (IMediator mediator, string eventId) =>
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

            group.MapGet("/event-types", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetEventTypesQuery());
                this.logger.LogInformation("Event types retrieved successfully.");
                return Results.Ok(result);
            });
        }
    }
}
