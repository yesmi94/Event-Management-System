// <copyright file="GetEventByIdQuery.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.GetEventById
{
    using EventManagementSystem.Application.DTOs.EventDtos;
    using EventManagementSystem.Application.Patterns;
    using MediatR;

    public record GetEventByIdQuery(string eventId)
        : IRequest<Result<GetEventDto>>;
}
