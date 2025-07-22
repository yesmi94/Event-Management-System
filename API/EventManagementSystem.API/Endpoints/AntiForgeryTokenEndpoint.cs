// <copyright file="AntiForgeryTokenEndpoint.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.API.Endpoints
{
    using Microsoft.AspNetCore.Antiforgery;
    using Microsoft.AspNetCore.Mvc;

    public static class AntiForgeryTokenEndpoint
    {
        public static void MapAntiforgeryEndpoints(this WebApplication app)
        {
            app.MapGet("api/antiforgery-token", ([FromServices] IAntiforgery antiforgery, HttpContext context) =>
            {
                var tokens = antiforgery.GetAndStoreTokens(context);
                return Results.Ok(new { token = tokens.RequestToken });
            });
        }
    }
}
