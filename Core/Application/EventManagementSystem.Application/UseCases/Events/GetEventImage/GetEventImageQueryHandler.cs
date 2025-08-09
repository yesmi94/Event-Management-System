// <copyright file="GetEventImageQueryHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.GetEventImage
{
    using EventManagementSystem.Application.Interfaces;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Domain.Entities;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class GetEventImageQueryHandler : IRequestHandler<GetEventImageQuery, Result<string>>
    {
        private readonly IRepository<EventImage> imageRepository;
        private readonly IUnitOfWork unitOfWork;

        public GetEventImageQueryHandler(IRepository<EventImage> imageRepository, IUnitOfWork unitOfWork)
        {
            this.imageRepository = imageRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(GetEventImageQuery request, CancellationToken cancellationToken)
        {
            var eventImage = await this.imageRepository.GetByIdAsync(request.eventId);

            if (eventImage == null)
            {
                return Result<string>.Failure("Failed to get the image for this event");
            }

            return Result<string>.Success(eventImage.Url!);
        }
    }
}
