// <copyright file="UserEndpoints.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.API.Endpoints
{
    using EventManagementSystem.API.Extensions;
    using EventManagementSystem.Application.DTOs.UserDtos;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Application.UseCases.Users.CreateUser;
    using EventManagementSystem.Application.UseCases.Users.GetUsers;
    using FluentValidation.Results;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class UserEndpoints : IEndpointGroup
    {
        private readonly ILogger<UserEndpoints> logger;

        public UserEndpoints(ILogger<UserEndpoints> logger)
        {
            this.logger = logger;
        }

        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/users");

            group.MapPost("/", async (IMediator mediator, [FromBody] CreateUserCommand command) =>
            {
                var result = await mediator.Send(command);

                if (!result.IsSuccess)
                {
                    var response = Response<List<ValidationFailure>>.FailureResponse([result.Error!], "Couldn't create the new User");
                    this.logger.LogWarning("Failed: Failed to create the new User. Error: {Error}", result.Error);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<GetUserDto>.SuccessResponse(result.Value!, $"New user - {result.Value!.Name}, {result.Value!.UserType} added successfully");
                this.logger.LogInformation("Registration - {result.Value} with {Role} added successfully", result.Value.Name, result.Value.UserType);
                return Results.Ok(successResponse);
            });

            group.MapGet("/", async (IMediator mediator) =>
            {
                var query = new GetUsersQuery();

                var result = await mediator.Send(query);

                if (!result.IsSuccess)
                {
                    var response = Response<List<GetUserDto>>.FailureResponse([result.Error!], "Couldn't get the uers list");
                    this.logger.LogWarning("Failed: Couldn't get the users list. Error: {Error}", result.Error);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<List<GetUserDto>>.SuccessResponse(result.Value!, $"Users list retrieved successfully");
                this.logger.LogInformation("Users list retrieved successfully");
                return Results.Ok(successResponse);
            });
        }
    }
}
