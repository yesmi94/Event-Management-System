// <copyright file="DashboardStatsDto.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.DTOs.DashboardDtos
{
    public class DashboardStatsDto
    {
        public int TotalEvents { get; set; }

        public int TotalRegistrations { get; set; }

        public int UpcomingEvents { get; set; }
    }
}
