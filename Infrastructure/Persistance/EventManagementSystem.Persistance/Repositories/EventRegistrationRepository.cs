// <copyright file="EventRegistrationRepository.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Persistance.Repositories
{
    using EventManagementSystem.Application.Interfaces;
    using EventManagementSystem.Domain.Entities;
    using EventManagementSystem.Persistance;
    using Microsoft.EntityFrameworkCore;

    public class EventRegistrationRepository : IEventRegistrationRepository
    {
        private readonly ApplicationDbContext context;

        public EventRegistrationRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<EventRegistration>> GetRegistrationsForUserWithEventAsync(string publicUserId)
        {
            return await this.context.Registrations
                .Include(r => r.Event)
                .Where(r => r.PublicUserId == publicUserId)
                .ToListAsync();
        }

        public async Task<bool> UserAlreadyRegisteredAsync(string publicUserId, string eventId)
        {
            return await this.context.Registrations
                .AnyAsync(r => r.PublicUserId == publicUserId && r.EventId == eventId);
        }
    }
}
