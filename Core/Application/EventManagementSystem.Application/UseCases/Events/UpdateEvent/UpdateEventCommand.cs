// <copyright file="UpdateEventCommand.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.UpdateEvent
{
    using EventManagementSystem.Application.DTOs.EventDtos;
    using EventManagementSystem.Application.Patterns;
    using MediatR;

    public record UpdateEventCommand(UpdateEventDto updateEventDto)
        : IRequest<Result<GetEventDto>>;
}
