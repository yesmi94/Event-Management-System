// <copyright file="UploadEventImageCommandHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.UploadEventImage
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventManagementSystem.Application.Interfaces;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Domain.Entities;
    using MediatR;

    public class UploadEventImageCommandHandler : IRequestHandler<UploadEventImageCommand, Result<string>>
    {
        private readonly IRepository<EventImage> imagesRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAzureBlobStorage azureBlobStorage;
        private readonly IRepository<Event> eventsRepository;

        public UploadEventImageCommandHandler(IRepository<EventImage> imagesRepository, IUnitOfWork unitOfWork, IAzureBlobStorage cloudStorage, IRepository<Event> eventsRepository)
        {
            this.imagesRepository = imagesRepository;
            this.unitOfWork = unitOfWork;
            this.azureBlobStorage = cloudStorage;
            this.eventsRepository = eventsRepository;
        }

        public async Task<Result<string>> Handle(UploadEventImageCommand request, CancellationToken cancellationToken)
        {
            var imageUrl = await this.azureBlobStorage.UploadImageAsync(request.imageFile);

            var image = new EventImage
            {
                EventId = request.eventId,
                Url = imageUrl,
            };

            await this.imagesRepository.AddAsync(image);

            var eventEntity = await this.eventsRepository.GetByIdAsync(request.eventId);
            if (eventEntity == null)
            {
                return Result<string>.Failure("Failed: Cannot find the event for this ID");
            }

            eventEntity.EventImageUrl = imageUrl;
            await this.unitOfWork.CompleteAsync();
            return Result<string>.Success(imageUrl);
        }
    }
}
