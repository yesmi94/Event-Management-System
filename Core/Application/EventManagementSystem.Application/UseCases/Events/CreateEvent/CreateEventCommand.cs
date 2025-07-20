// <copyright file="CreateEventCommand.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.CreateEvent
{
    using EventManagementSystem.Application.DTOs.EventDtos;
    using EventManagementSystem.Application.Patterns;
    using MediatR;

    public record CreateEventCommand(NewEventDto newEventDto)
        : IRequest<Result<GetEventDto>>;
}
