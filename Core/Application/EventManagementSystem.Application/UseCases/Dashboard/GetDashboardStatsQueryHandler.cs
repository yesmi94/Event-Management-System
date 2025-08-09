// <copyright file="GetDashboardStatsQueryHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Dashboard
{
    using EventManagementSystem.Application.DTOs.DashboardDtos;
    using EventManagementSystem.Application.Interfaces;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Domain.Entities;
    using MediatR;

    public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, Result<DashboardStatsDto>>
    {
        private readonly IRepository<Event> eventsRepository;
        private readonly IRepository<EventRegistration> eventsRegistrationsRepository;

        public GetDashboardStatsQueryHandler(IRepository<Event> eventsRepository, IRepository<EventRegistration> eventsRegistrationsRepository)
        {
            this.eventsRepository = eventsRepository;
            this.eventsRegistrationsRepository = eventsRegistrationsRepository;
        }

        public async Task<Result<DashboardStatsDto>> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;

            var dashboardStats = new DashboardStatsDto
            {
                TotalEvents = await this.eventsRepository.CountAsync(),
                TotalRegistrations = await this.eventsRegistrationsRepository.CountAsync(),
                UpcomingEvents = await this.eventsRepository.CountAsync(e => e.EventDate >= now),
            };

            return Result<DashboardStatsDto>.Success(dashboardStats);
        }
    }
}
