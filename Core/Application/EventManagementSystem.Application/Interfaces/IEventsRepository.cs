// <copyright file="IEventsRepository.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.Interfaces
{
    using System.Linq.Expressions;
    using EventManagementSystem.Application.DTOs.EventDtos;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Domain.Entities;

    public interface IEventsRepository
    {
        Task<PaginatedResult<GetEventDto>> SearchEventsAsync(
        string? search,
        string? category,
        string? location,
        string? status,
        DateTime? dateFrom,
        DateTime? dateTo,
        int page,
        int pageSize);

        Task<(List<Event> Items, int TotalCount)> GetFilteredPagedAsync(
        Expression<Func<Event, bool>>? predicate,
        int page,
        int pageSize);
    }
}
