// <copyright file="Event.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

using EventManagementSystem.Domain.Enums;

namespace EventManagementSystem.Domain.Entities
{
    public class Event
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        public string? CreatedByUserId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime EventDate { get; set; }

        public TimeSpan EventTime { get; set; }

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        public string? Location { get; set; }

        public EventType? Type { get; set; }

        public string? Organization { get; set; }

        public int Capacity { get; set; }

        public DateTime CutoffDate { get; set; }

        public virtual User? User { get; set; }
    }
}
