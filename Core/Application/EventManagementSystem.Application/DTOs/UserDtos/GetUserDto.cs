// <copyright file="GetUserDto.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.DTOs.UserDtos
{
    using EventManagementSystem.Domain.Enums;

    public class GetUserDto
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public UserType? UserType { get; set; }
    }
}
