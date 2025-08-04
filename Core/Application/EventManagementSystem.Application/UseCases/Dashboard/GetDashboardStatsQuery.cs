// <copyright file="GetDashboardStatsQuery.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Dashboard
{
    using EventManagementSystem.Application.DTOs.DashboardDtos;
    using EventManagementSystem.Application.Patterns;
    using MediatR;

    public record GetDashboardStatsQuery : IRequest<Result<DashboardStatsDto>>;
}
