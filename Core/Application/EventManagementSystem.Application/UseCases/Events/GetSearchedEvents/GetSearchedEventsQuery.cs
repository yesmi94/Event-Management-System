// <copyright file="GetSearchedEventsQuery.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.GetSearchedEvents
{
    using EventManagementSystem.Application.DTOs.EventDtos;
    using EventManagementSystem.Application.Patterns;
    using MediatR;

    public record GetSearchedEventsQuery(int page, int pageSize, string search)
        : IRequest<Result<PaginatedResult<GetEventDto>>>;
}
