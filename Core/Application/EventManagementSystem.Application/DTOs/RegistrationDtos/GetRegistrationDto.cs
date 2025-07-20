// <copyright file="GetRegistrationDto.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.DTOs.RegistrationDtos
{
    public class GetRegistrationDto(string? email)
    {
        public string? RegisteredUserName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; } = email;
    }
}
