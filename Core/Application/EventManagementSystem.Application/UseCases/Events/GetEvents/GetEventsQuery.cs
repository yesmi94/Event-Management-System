// <copyright file="GetEventsQuery.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.GetEvents
{
    using EventManagementSystem.Application.DTOs.EventDtos;
    using EventManagementSystem.Application.Patterns;
    using MediatR;

    public record GetEventsQuery()
        : IRequest<Result<List<GetEventDto>>>;
}
