// <copyright file="GetEventImageQuery.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.GetEventImage
{
    using EventManagementSystem.Application.Patterns;
    using MediatR;

    public record GetEventImageQuery(string eventId)
        : IRequest<Result<string>>;
}
