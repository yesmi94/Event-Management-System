// <copyright file="DashboardEndpoints.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.API.Endpoints
{
    using EventManagementSystem.API.Extensions;
    using EventManagementSystem.Application.DTOs.DashboardDtos;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Application.UseCases.Dashboard;
    using MediatR;

    public class DashboardEndpoints : IEndpointGroup
    {
        private readonly ILogger<EventEndpoints> logger;

        public DashboardEndpoints(ILogger<EventEndpoints> logger)
        {
            this.logger = logger;
        }

        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/dashboard", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetDashboardStatsQuery());

                if (!result.IsSuccess)
                {
                    var response = Response<string>.FailureResponse([result.Error!], "Couldn't load the dashboard stats");
                    this.logger.LogWarning("Failed: Failed load the dashboard stats. Error: {Error}", result.Error);
                    return Results.BadRequest(response);
                }

                var successResponse = Response<DashboardStatsDto>.SuccessResponse(result.Value!);
                this.logger.LogInformation("Retrieved dashboard stats succuessfully {Dashboard satats}", result.Value);
                return Results.Ok(successResponse);
            });
        }
    }
}
