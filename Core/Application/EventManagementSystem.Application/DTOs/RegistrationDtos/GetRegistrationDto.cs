// <copyright file="GetRegistrationDto.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.DTOs.RegistrationDtos
{
    using EventManagementSystem.Domain.Entities;

    public class GetRegistrationDto(string? email)
    {
        public string? Id { get; set; }

        public string? EventId { get; set; }

        public string? PublicUserId { get; set; }

        public string? RegisteredUserName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; } = email;

        public DateTime RegisteredAt { get; set; }

        public Event? Event { get; set; }
    }
}
