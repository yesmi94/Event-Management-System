// <copyright file="IEventRegistrationRepository.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using EventManagementSystem.Domain.Entities;

    public interface IEventRegistrationRepository
    {
        Task<List<EventRegistration>> GetRegistrationsForUserWithEventAsync(string publicUserId);

        Task<bool> UserAlreadyRegisteredAsync(string publicUserId, string eventId);
    }
}
