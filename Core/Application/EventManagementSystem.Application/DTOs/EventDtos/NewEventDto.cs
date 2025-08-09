// <copyright file="NewEventDto.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.DTOs.EventDtos
{
    using EventManagementSystem.Domain.Enums;

    public class NewEventDto
    {
        public string? Title { get; set; }

        public string? CreatedByUserId { get; set; }

        public string? EventImageUrl { get; set; }

        public string? Description { get; set; }

        public DateTime EventDate { get; set; }

        public TimeSpan EventTime { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public string? Location { get; set; }

        public EventType? Type { get; set; }

        public string? Organization { get; set; }

        public int Capacity { get; set; }

        public DateTime RegisteredAt { get; set; }

        public DateTime CutoffDate { get; set; }
    }
}
