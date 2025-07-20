// <copyright file="CancelRegistrationCommand.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.EventRegistrations.CancelRegistration
{
    using EventManagementSystem.Application.Patterns;
    using MediatR;

    public record CancelRegistrationCommand(string registrationId)
        : IRequest<Result<string>>;
}
