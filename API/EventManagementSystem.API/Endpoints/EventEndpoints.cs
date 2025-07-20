// <copyright file="EventEndpoints.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.API.Endpoints
{
    using EventManagementSystem.API.Extensions;
    using EventManagementSystem.Application.DTOs.EventDtos;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Application.UseCases.Events.CreateEvent;
    using EventManagementSystem.Application.UseCases.Events.DeleteEvent;
    using EventManagementSystem.Application.UseCases.Events.GetEvents;
    using EventManagementSystem.Application.UseCases.Events.UpdateEvent;
    using MediatR;
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

            group.MapPost("/", async (IMediator mediator, [FromBody] CreateEventCommand command) =>
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

            group.MapGet("/", async (IMediator mediator) =>
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

            group.MapPut("/{eventId}", async (IMediator mediator, [FromBody] UpdateEventCommand command, [FromRoute] string eventId) =>
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

            group.MapDelete("/{eventId}", async (IMediator mediator, [FromRoute] string eventId) =>
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
        }
    }
}
