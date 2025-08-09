// <copyright file="GetEventTypesQuery.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.GetEventType
{
    using EventManagementSystem.Application.DTOs.EventDtos;
    using EventManagementSystem.Application.Patterns;
    using MediatR;

    public class GetEventTypesQuery : IRequest<Result<List<EventTypeDto>>>
    {
    }
}
