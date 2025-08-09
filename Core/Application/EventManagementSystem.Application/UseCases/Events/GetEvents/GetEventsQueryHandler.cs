// <copyright file="GetEventsQueryHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.GetEvents
{
    using AutoMapper;
    using EventManagementSystem.Application.DTOs.EventDtos;
    using EventManagementSystem.Application.Interfaces;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Domain.Entities;
    using MediatR;

    public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, Result<PaginatedResult<GetEventDto>>>
    {
        private readonly IRepository<Event> eventsRepository;
        private readonly IMapper mapper;

        public GetEventsQueryHandler(IRepository<Event> eventsRepository, IMapper mapper)
        {
            this.eventsRepository = eventsRepository;
            this.mapper = mapper;
        }

        public async Task<Result<PaginatedResult<GetEventDto>>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            var allEventsQueryable = await this.eventsRepository.GetAllAsync();

            var totalCount = allEventsQueryable.Count();

            var pagedEvents = allEventsQueryable
                .OrderByDescending(e => e.EventDate)
                .Skip((request.page - 1) * request.pageSize)
                .Take(request.pageSize)
                .ToList();

            var eventDtos = this.mapper.Map<List<GetEventDto>>(pagedEvents);

            var result = new PaginatedResult<GetEventDto>(eventDtos, totalCount, request.page, request.pageSize);

            return Result<PaginatedResult<GetEventDto>>.Success(result);
        }
    }
}
