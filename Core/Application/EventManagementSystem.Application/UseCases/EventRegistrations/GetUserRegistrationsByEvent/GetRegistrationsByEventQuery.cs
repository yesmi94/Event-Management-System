// <copyright file="GetRegistrationsByEventQuery.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.EventRegistrations.GetUserRegistrationsByEvent
{
    using EventManagementSystem.Application.DTOs.RegistrationDtos;
    using EventManagementSystem.Application.Patterns;
    using MediatR;

    public record GetRegistrationsByEventQuery(string eventId)
        : IRequest<Result<List<GetRegistrationDto>>>;
}
