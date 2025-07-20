// <copyright file="CreateEventCommandHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.CreateEvent
{
    using AutoMapper;
    using EventManagementSystem.Application.DTOs.EventDtos;
    using EventManagementSystem.Application.Interfaces;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Domain.Entities;
    using MediatR;

    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Result<GetEventDto>>
    {
        private readonly IRepository<Event> eventsRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateEventCommandHandler(IRepository<Event> eventsRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.eventsRepository = eventsRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<GetEventDto>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return Result<GetEventDto>.Failure("Event data cannot be null");
            }

            if (request.newEventDto == null)
            {
                return Result<GetEventDto>.Failure("dto cannot be null");
            }

            var requestDto = request.newEventDto;
            Event newEvent = new Event
            {
                Title = requestDto.Title,
                CreatedByUserId = requestDto.CreatedByUserId,
                Description = requestDto.Description,
                EventDate = requestDto.EventDate,
                EventTime = requestDto.EventTime,
                Location = requestDto.Location,
                Organization = requestDto.Organization,
                Capacity = requestDto.Capacity,
                CutoffDate = requestDto.CutoffDate,
                Type = requestDto.Type,
            };

            var getEventDto = this.mapper.Map<GetEventDto>(requestDto);

            await this.eventsRepository.AddAsync(newEvent);
            await this.unitOfWork.CompleteAsync();
            return Result<GetEventDto>.Success(getEventDto);
        }
    }
}
