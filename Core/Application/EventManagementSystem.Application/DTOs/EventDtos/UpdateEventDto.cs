// <copyright file="UpdateEventDto.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.DTOs.EventDtos
{
    using EventManagementSystem.Domain.Enums;

    public class UpdateEventDto
    {
        public string? Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? EventImageUrl { get; set; }

        public DateTime EventDate { get; set; }

        public TimeSpan EventTime { get; set; }

        public string? Location { get; set; }

        public EventType? Type { get; set; }

        public string? Organization { get; set; }

        public int Capacity { get; set; }

        public DateTime CutoffDate { get; set; }
    }
}
