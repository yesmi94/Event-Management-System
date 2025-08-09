// <copyright file="EventConfiguration.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Persistance.Configurations
{
    using EventManagementSystem.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(evt => evt.Id);

            builder.Property(evt => evt.CreatedByUserId)
                .IsRequired();

            builder.Property(evt => evt.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(evt => evt.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(evt => evt.EventDate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(evt => evt.EventTime)
                .IsRequired()
                .HasColumnType("time");

            builder.Property(evt => evt.CreatedTime)
                .IsRequired();

            builder.Property(evt => evt.Location)
                .IsRequired();

            builder.Property(evt => evt.Type)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(evt => evt.Organization)
                .IsRequired();

            builder.Property(evt => evt.Capacity)
               .IsRequired()
               .HasColumnType("int");

            builder.Property(evt => evt.CutoffDate)
               .IsRequired()
               .HasColumnType("date");
        }
    }
}
