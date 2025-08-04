// <copyright file="GetSearchedEventsQueryHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.GetSearchedEvents
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using EventManagementSystem.Application.DTOs.EventDtos;
    using EventManagementSystem.Application.Interfaces;
    using EventManagementSystem.Application.Patterns;
    using MediatR;

    public class GetSearchedEventsQueryHandler : IRequestHandler<GetSearchedEventsQuery, Result<PaginatedResult<GetEventDto>>>
    {
        private readonly IEventsRepository eventsRepository;
        private readonly IMapper mapper;

        public GetSearchedEventsQueryHandler(IEventsRepository eventsRepository, IMapper mapper)
        {
            this.eventsRepository = eventsRepository;
            this.mapper = mapper;
        }

        public async Task<Result<PaginatedResult<GetEventDto>>> Handle(GetSearchedEventsQuery request, CancellationToken cancellationToken)
        {
            var (events, totalCount) = await this.eventsRepository.GetFilteredPagedAsync(
                e => e.Title.Contains(request.search), request.page, request.pageSize);

            var eventDtos = this.mapper.Map<List<GetEventDto>>(events);

            var result = new PaginatedResult<GetEventDto>(eventDtos, totalCount, request.page, request.pageSize);
            return Result<PaginatedResult<GetEventDto>>.Success(result);
        }
    }
}
