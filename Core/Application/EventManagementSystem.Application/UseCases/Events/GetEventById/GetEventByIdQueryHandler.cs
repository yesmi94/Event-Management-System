// <copyright file="GetEventByIdQueryHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.GetEventById
{
    using AutoMapper;
    using EventManagementSystem.Application.DTOs.EventDtos;
    using EventManagementSystem.Application.Interfaces;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Domain.Entities;
    using MediatR;

    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, Result<GetEventDto>>
    {
        private readonly IRepository<Event> eventsRepository;
        private readonly IMapper mapper;

        public GetEventByIdQueryHandler(IRepository<Event> eventsRepository, IMapper mapper)
        {
            this.eventsRepository = eventsRepository;
            this.mapper = mapper;
        }

        public async Task<Result<GetEventDto>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var eventById = await this.eventsRepository.GetByIdAsync(request.eventId);

            var eventByIdDto = this.mapper.Map<GetEventDto>(eventById);
            return Result<GetEventDto>.Success(eventByIdDto);
        }
    }
}
