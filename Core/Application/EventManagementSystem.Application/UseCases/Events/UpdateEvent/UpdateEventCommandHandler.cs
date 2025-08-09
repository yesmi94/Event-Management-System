// <copyright file="UpdateEventCommandHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.UpdateEvent
{
    using AutoMapper;
    using EventManagementSystem.Application.DTOs.EventDtos;
    using EventManagementSystem.Application.Interfaces;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Domain.Entities;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Result<GetEventDto>>
    {
        private readonly IRepository<Event> eventsRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<UpdateEventCommandHandler> eventsRepository2;

        public UpdateEventCommandHandler(IRepository<Event> eventsRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateEventCommandHandler> eventsRepository2)
        {
            this.eventsRepository = eventsRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.eventsRepository2 = eventsRepository2;
        }

        public async Task<Result<GetEventDto>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var requestDto = request.updateEventDto;

            this.eventsRepository2.LogError(requestDto.Id!);

            var existingEvent = await this.eventsRepository.GetByIdAsync(requestDto.Id!);

            if (existingEvent == null)
            {
                return Result<GetEventDto>.Failure("Event not found.");
            }

            existingEvent.Id = requestDto.Id!;
            existingEvent.Title = requestDto.Title;
            existingEvent.Description = requestDto.Description;
            existingEvent.EventDate = requestDto.EventDate;
            existingEvent.EventTime = requestDto.EventTime;
            existingEvent.Location = requestDto.Location;
            existingEvent.Organization = requestDto.Organization;
            existingEvent.Capacity = requestDto.Capacity;
            existingEvent.CutoffDate = requestDto.CutoffDate;

            await this.eventsRepository.UpdateAsync(existingEvent);
            await this.unitOfWork.CompleteAsync();

            var getEventDto = this.mapper.Map<GetEventDto>(existingEvent);
            return Result<GetEventDto>.Success(getEventDto);
        }
    }
}
