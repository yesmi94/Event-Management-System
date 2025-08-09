// <copyright file="UserEndpoints.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.API.Endpoints
{
    using System.Security.Claims;
    using EventManagementSystem.API.Extensions;
    using EventManagementSystem.Application.DTOs.RegistrationDtos;
    using EventManagementSystem.Application.DTOs.UserDtos;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Application.UseCases.Users.CreateUser;
    using EventManagementSystem.Application.UseCases.Users.GetRegistrationsForUser;
    using EventManagementSystem.Application.UseCases.Users.GetUsers;
    using FluentValidation.Results;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
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
            var group = app.MapGroup("api/user");

            group.MapGet(
                "/registrations",
                [Authorize(Roles = "Public User")]
                async (IMediator mediator, HttpContext httpContext) =>
                {
                    var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                    var query = new GetRegistrationsForUserQuery(userId!);

                    var result = await mediator.Send(query);

                    if (!result.IsSuccess)
                    {
                        var response = Response<List<GetRegistrationDto>>.FailureResponse([result.Error!], "Couldn't get the registrations for the user");
                        this.logger.LogWarning("Failed: Couldn't get registrations for the user. Error: {Error}", result.Error);
                        return Results.BadRequest(response);
                    }

                    var successResponse = Response<List<GetRegistrationDto>>.SuccessResponse(result.Value!, $"Registrations for the user retrieved successfully");
                    this.logger.LogInformation("Registrations for the user retrieved successfully");
                    return Results.Ok(successResponse);
                });
        }
    }
}
