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

    public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, Result<List<GetEventDto>>>
    {
        private readonly IRepository<Event> eventsRepository;
        private readonly IMapper mapper;

        public GetEventsQueryHandler(IRepository<Event> eventsRepository, IMapper mapper)
        {
            this.eventsRepository = eventsRepository;
            this.mapper = mapper;
        }

        public async Task<Result<List<GetEventDto>>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            var allEvents = await this.eventsRepository.GetAllAsync();

            var allEventsList = this.mapper.Map<List<GetEventDto>>(allEvents);
            return Result<List<GetEventDto>>.Success(allEventsList);
        }
    }
}
