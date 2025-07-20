// <copyright file="EventRegistration.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Domain.Entities
{
    public class EventRegistration
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString("N");

        public string? EventId { get; set; }

        public virtual User? User { get; set; }

        public string? PublicUserId { get; set; }

        public string? RegisteredUserName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public virtual Event? RegisteredEvent { get; set; }
    }
}
