// <copyright file="EventImageConfiguration.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Persistance.Configurations
{
    using EventManagementSystem.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class EventImageConfiguration
    {
        public void Configure(EntityTypeBuilder<EventImage> builder)
        {
            builder.ToTable("EventImages");

            builder.HasKey(eventImage => eventImage.Id);

            builder.Property(e => e.Url)
            .HasMaxLength(500);

            builder.HasOne(e => e.Event)
            .WithMany()
            .HasForeignKey(e => e.EventId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
