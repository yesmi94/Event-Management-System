// <copyright file="CreateUserCommand.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Users.CreateUser
{
    using EventManagementSystem.Application.DTOs.UserDtos;
    using EventManagementSystem.Application.Patterns;
    using MediatR;

    public record CreateUserCommand(NewUserDto newUserDto)
        : IRequest<Result<GetUserDto>>;
}
