// <copyright file="GetRegistrationsForUserQueryHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Users.GetRegistrationsForUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using EventManagementSystem.Application.DTOs.RegistrationDtos;
    using EventManagementSystem.Application.Interfaces;
    using EventManagementSystem.Application.Patterns;
    using MediatR;

    public class GetRegistrationsForUserQueryHandler : IRequestHandler<GetRegistrationsForUserQuery, Result<List<GetRegistrationDto>>>
    {
        private readonly IEventRegistrationRepository eventRegistrationsRepository;
        private readonly IMapper mapper;

        public GetRegistrationsForUserQueryHandler(IEventRegistrationRepository eventRegistrationsRepository, IMapper mapper)
        {
            this.eventRegistrationsRepository = eventRegistrationsRepository;
            this.mapper = mapper;
        }

        public async Task<Result<List<GetRegistrationDto>>> Handle(GetRegistrationsForUserQuery request, CancellationToken cancellationToken)
        {
            var userId = request.userId;
            var registrations = await this.eventRegistrationsRepository.GetRegistrationsForUserWithEventAsync(userId);

            var registrationDtos = this.mapper.Map<List<GetRegistrationDto>>(registrations);

            return Result<List<GetRegistrationDto>>.Success(registrationDtos);
        }
    }
}
