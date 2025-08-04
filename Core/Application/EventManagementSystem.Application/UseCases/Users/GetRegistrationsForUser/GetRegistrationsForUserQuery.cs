// <copyright file="GetRegistrationsForUserQuery.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Users.GetRegistrationsForUser
{
    using EventManagementSystem.Application.DTOs.RegistrationDtos;
    using EventManagementSystem.Application.Patterns;
    using MediatR;

    public record GetRegistrationsForUserQuery(string userId)
        : IRequest<Result<List<GetRegistrationDto>>>;
}
