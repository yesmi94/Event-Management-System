// <copyright file="DeleteEventCommand.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.DeleteEvent
{
    using EventManagementSystem.Application.Patterns;
    using MediatR;

    public record DeleteEventCommand(string eventId)
        : IRequest<Result<string>>;
}
