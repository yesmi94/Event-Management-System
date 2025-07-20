// <copyright file="GetRegistrationsByEventQueryHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.EventRegistrations.GetUserRegistrationsByEvent
{
    using System.Collections.Generic;
    using AutoMapper;
    using EventManagementSystem.Application.DTOs.RegistrationDtos;
    using EventManagementSystem.Application.Interfaces;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Domain.Entities;
    using MediatR;

    public class GetRegistrationsByEventQueryHandler : IRequestHandler<GetRegistrationsByEventQuery, Result<List<GetRegistrationDto>>>
    {
        private readonly IRepository<EventRegistration> registrationsRepository;

        private readonly IMapper mapper;

        public GetRegistrationsByEventQueryHandler(IRepository<EventRegistration> registrationsRepository, IMapper mapper)
        {
            this.registrationsRepository = registrationsRepository;
            this.mapper = mapper;
        }

        public async Task<Result<List<GetRegistrationDto>>> Handle(GetRegistrationsByEventQuery request, CancellationToken cancellationToken)
        {
            var registrationsForEvent = await this.registrationsRepository
                    .FindAsync(reg => reg.EventId == request.eventId);

            var registrationsForEventList = this.mapper.Map<List<GetRegistrationDto>>(registrationsForEvent);

            return Result<List<GetRegistrationDto>>.Success(registrationsForEventList);
        }
    }
}
