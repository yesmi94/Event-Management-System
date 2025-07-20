// <copyright file="UserConfiguration.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Persistance.Configurations
{
    using EventManagementSystem.Domain.Entities;
    using EventManagementSystem.Domain.Enums;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasDiscriminator<UserType>("Role")

                .HasValue<User>(UserType.Admin)
                .HasValue<User>(UserType.PublicUser);

            builder.HasKey(user => user.Id);

            builder.Property(user => user.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(user => user.UserType)
                .IsRequired()
                .HasConversion<string>();
        }
    }
}
