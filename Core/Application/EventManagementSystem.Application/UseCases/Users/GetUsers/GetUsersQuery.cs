// <copyright file="GetUsersQuery.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Users.GetUsers
{
    using EventManagementSystem.Application.DTOs.UserDtos;
    using EventManagementSystem.Application.Patterns;
    using MediatR;

    public record GetUsersQuery()
        : IRequest<Result<List<GetUserDto>>>;
}
