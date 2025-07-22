// <copyright file="UploadEventImageCommand.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.UploadEventImage
{
    using EventManagementSystem.Application.Patterns;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public record UploadEventImageCommand(string eventId, IFormFile imageFile)
        : IRequest<Result<string>>;
}
