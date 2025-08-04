// <copyright file="EventRegistrationConfiguration.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Persistance.Configurations
{
    using EventManagementSystem.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class EventRegistrationConfiguration : IEntityTypeConfiguration<EventRegistration>
    {
        public void Configure(EntityTypeBuilder<EventRegistration> builder)
        {
            builder.ToTable("EventRegistrations");

            builder.HasKey(eventRegistration => eventRegistration.Id);

            builder.Property(eventRegistration => eventRegistration.PublicUserId)
                .IsRequired();

            builder.Property(eventRegistration => eventRegistration.RegisteredUserName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(eventRegistration => eventRegistration.PhoneNumber)
                .IsRequired();

            builder.Property(eventRegistration => eventRegistration.Email)
                .IsRequired();

            builder.HasOne<Event>()
                .WithMany()
                .HasForeignKey(reg => reg.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(reg => reg.Event)
                .WithMany()
                .HasForeignKey(reg => reg.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasIndex(r => new { r.PublicUserId, r.EventId })
                .IsUnique();
        }
    }
}
