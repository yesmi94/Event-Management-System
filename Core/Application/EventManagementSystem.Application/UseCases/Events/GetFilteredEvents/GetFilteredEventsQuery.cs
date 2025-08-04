// <copyright file="GetFilteredEventsQuery.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.GetFilteredEvents
{
    using EventManagementSystem.Application.DTOs.EventDtos;
    using EventManagementSystem.Application.Patterns;
    using MediatR;

    public record GetFilteredEventsQuery(
        int page,
        int pageSize,
        string? search,
        string? category,
        string? location,
        string? status,
        DateTime? dateFrom,
        DateTime? dateTo)
        : IRequest<Result<PaginatedResult<GetEventDto>>>;
}
