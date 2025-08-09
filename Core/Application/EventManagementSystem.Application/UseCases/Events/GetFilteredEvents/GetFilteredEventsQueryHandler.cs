// <copyright file="GetFilteredEventsQueryHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.GetFilteredEvents
{
    using AutoMapper;
    using EventManagementSystem.Application.DTOs.EventDtos;
    using EventManagementSystem.Application.Interfaces;
    using EventManagementSystem.Application.Patterns;
    using MediatR;

    public class GetFilteredEventsQueryHandler : IRequestHandler<GetFilteredEventsQuery, Result<PaginatedResult<GetEventDto>>>
    {
        private readonly IEventsRepository eventsRepository;
        private readonly IMapper mapper;

        public GetFilteredEventsQueryHandler(IEventsRepository eventsRepository, IMapper mapper)
        {
            this.eventsRepository = eventsRepository;
            this.mapper = mapper;
        }

        public async Task<Result<PaginatedResult<GetEventDto>>> Handle(GetFilteredEventsQuery request, CancellationToken cancellationToken)
        {
            var result = await this.eventsRepository.SearchEventsAsync(
            request.search,
            request.category,
            request.location,
            request.status,
            request.dateFrom,
            request.dateTo,
            request.page,
            request.pageSize);

            return Result<PaginatedResult<GetEventDto>>.Success(result);
        }
    }
}
