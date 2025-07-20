// <copyright file="NewUserDto.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.DTOs.UserDtos
{
    using EventManagementSystem.Domain.Enums;

    public class NewUserDto
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public UserType? UserType { get; set; }
    }
}
