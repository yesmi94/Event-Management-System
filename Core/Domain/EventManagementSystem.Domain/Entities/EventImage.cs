// <copyright file="EventImage.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Domain.Entities
{
    public class EventImage
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        public string? EventId { get; set; }

        public string? Url { get; set; }
    }
}
