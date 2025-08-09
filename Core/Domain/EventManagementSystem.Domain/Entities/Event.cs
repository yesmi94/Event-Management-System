// <copyright file="Event.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Domain.Entities
{
    using EventManagementSystem.Domain.Enums;

    public class Event
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        public string CreatedByUserId { get; set; } = default!;

        public string? Title { get; set; }

        public string? EventImageUrl { get; set; }

        public string? Description { get; set; }

        public DateTime EventDate { get; set; }

        public TimeSpan EventTime { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        public string? Location { get; set; }

        public EventType? Type { get; set; }

        public string? Organization { get; set; }

        public int Capacity { get; set; }

        public DateTime CutoffDate { get; set; }

        public int RemainingSpots { get; set; }
    }
}
