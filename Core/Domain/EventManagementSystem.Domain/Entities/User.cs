// <copyright file="User.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Domain.Entities
{
    using EventManagementSystem.Domain.Enums;

    public class User
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString("N");

        public string? Name { get; set; }

        public string? Email { get; set; }

        public UserType? UserType { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
