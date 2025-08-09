// <copyright file="EventRegistration.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Domain.Entities
{
    public class EventRegistration
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString("N");

        public string? EventId { get; set; }

        public string? PublicUserId { get; set; }

        public string? RegisteredUserName { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        public string? Email { get; set; }

        public virtual Event? Event { get; set; }
    }
}
