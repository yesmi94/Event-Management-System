// <copyright file="CreateEventRegistrationCommand.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.EventRegistrations.CreateEventRegistration
{
    using EventManagementSystem.Application.DTOs.RegistrationDtos;
    using EventManagementSystem.Application.Patterns;
    using MediatR;

    public record CreateEventRegistrationCommand(NewRegistrationDto newRegistrationDto)
        : IRequest<Result<GetRegistrationDto>>;
}
