// <copyright file="NewRegistrationDto.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.DTOs.RegistrationDtos
{
    public class NewRegistrationDto
    {
        public string? PublicUserId { get; set; }

        public string? EventId { get; set; }

        public string? RegisteredUserName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public DateTime RegisteredAt { get; set; }
    }
}
