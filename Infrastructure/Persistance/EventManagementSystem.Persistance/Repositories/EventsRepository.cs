// <copyright file="EventsRepository.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Persistance.Repositories
{
    using System.Linq;
    using System.Linq.Expressions;
    using EventManagementSystem.Application.DTOs.EventDtos;
    using EventManagementSystem.Application.Interfaces;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Domain.Entities;
    using EventManagementSystem.Domain.Enums;
    using Microsoft.EntityFrameworkCore;

    public class EventsRepository : IEventsRepository
    {
        private readonly ApplicationDbContext context;

        public EventsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<PaginatedResult<GetEventDto>> SearchEventsAsync(
        string? search,
        string? category,
        string? location,
        string? status,
        DateTime? dateFrom,
        DateTime? dateTo,
        int page,
        int pageSize)
        {
            var query = this.context.Events.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e => e.Title!.Contains(search) || e.Description!.Contains(search));
            }

            if (Enum.TryParse<EventType>(category, true, out var parsedCategory))
            {
                query = query.Where(e => e.Type == parsedCategory);
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                query = query.Where(e => e.Location!.ToLower() == location.ToLower());
            }

            if (dateFrom.HasValue)
            {
                query = query.Where(e => e.EventDate >= dateFrom.Value);
            }

            if (dateTo.HasValue)
            {
                query = query.Where(e => e.EventDate <= dateTo.Value);
            }

            var today = DateTime.UtcNow.Date;
            query = status switch
            {
                "Upcoming" => query.Where(e => e.EventDate > today),
                "Past" => query.Where(e => e.EventDate < today),
                "Today" => query.Where(e => e.EventDate.Date == today),
                _ => query
            };

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(e => e.EventDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new GetEventDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    CutoffDate = e.CutoffDate,
                    EventDate = e.EventDate,
                    EventTime = e.EventTime,
                    Type = e.Type.ToString(),
                    Location = e.Location,
                    EventImageUrl = e.EventImageUrl,
                    RemainingSpots = e.RemainingSpots,
                    Capacity = e.Capacity,
                })
                .ToListAsync();

            return new PaginatedResult<GetEventDto>(items, totalCount, page, pageSize);
        }

        public async Task<(List<Event> Items, int TotalCount)> GetFilteredPagedAsync(
        Expression<Func<Event, bool>>? predicate,
        int page,
        int pageSize)
        {
            var query = this.context.Events.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
