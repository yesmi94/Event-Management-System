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
        private readonly IRepository<EventRegistration> eventRegistrationepository;
        private readonly IRepository<Event> eventRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateEventRegistrationCommandHandler(IRepository<EventRegistration> eventRegistrationepository, IRepository<Event> eventRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.eventRegistrationepository = eventRegistrationepository;
            this.eventRepository = eventRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<GetRegistrationDto>> Handle(CreateEventRegistrationCommand request, CancellationToken cancellationToken)
        {
            var requestDto = request.newRegistrationDto;

            var registrationEvent = await this.eventRepository.GetByIdAsync(requestDto.EventId!);

            if (registrationEvent == null)
            {
                return Result<GetRegistrationDto>.Failure("Failed: This Event doesn't exist. Try using a valid Event ID");
            }

            EventRegistration registration = new EventRegistration
            {
                EventId = requestDto.EventId,
                PublicUserId = requestDto.PublicUserId,
                RegisteredUserName = requestDto.RegisteredUserName,
                PhoneNumber = requestDto.PhoneNumber,
                Email = requestDto.Email,
            };

            GetRegistrationDto registrationDto = this.mapper.Map<GetRegistrationDto>(registration);

            await this.eventRegistrationepository.AddAsync(registration);
            await this.unitOfWork.CompleteAsync();

            return Result<GetRegistrationDto>.Success(registrationDto);
        }
    }
}
