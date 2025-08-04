// <copyright file="CreateEventRegistrationCommandHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.EventRegistrations.CreateEventRegistration
{
    using AutoMapper;
    using EventManagementSystem.Application.DTOs.RegistrationDtos;
    using EventManagementSystem.Application.Interfaces;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Domain.Entities;
    using MediatR;

    public class CreateEventRegistrationCommandHandler : IRequestHandler<CreateEventRegistrationCommand, Result<GetRegistrationDto>>
    {
        private readonly IRepository<EventRegistration> eventRegistrationRepository;
        private readonly IRepository<Event> eventRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateEventRegistrationCommandHandler(IRepository<EventRegistration> eventRegistrationRepository, IRepository<Event> eventRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.eventRegistrationRepository = eventRegistrationRepository;
            this.eventRepository = eventRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<GetRegistrationDto>> Handle(CreateEventRegistrationCommand request, CancellationToken cancellationToken)
        {
            var today = DateTime.UtcNow.Date;

            int currentRegistrations = await this.eventRegistrationRepository.CountAsync();

            var requestDto = request.newRegistrationDto;

            var registrationEvent = await this.eventRepository.GetByIdAsync(requestDto.EventId!);

            var existingRegistrationsList = await this.eventRegistrationRepository
                .FindAsync(r => r.EventId == requestDto.EventId && r.PublicUserId == requestDto.PublicUserId);

            if (existingRegistrationsList.Any())
            {
                return Result<GetRegistrationDto>.Failure("Failed: You have already registered for this event.");
            }

            if (currentRegistrations == registrationEvent!.Capacity)
            {
                return Result<GetRegistrationDto>.Failure("Sorry! The Event is Fully Booked.");
            }

            EventRegistration registration = new EventRegistration
            {
                EventId = requestDto.EventId,
                PublicUserId = requestDto.PublicUserId,
                RegisteredUserName = requestDto.RegisteredUserName,
                PhoneNumber = requestDto.PhoneNumber,
                Email = requestDto.Email,
                RegisteredAt = DateTime.UtcNow,
            };

            GetRegistrationDto registrationDto = this.mapper.Map<GetRegistrationDto>(registration);

            await this.eventRegistrationRepository.AddAsync(registration);
            await this.unitOfWork.CompleteAsync();

            return Result<GetRegistrationDto>.Success(registrationDto);
        }
    }
}
