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

        public UploadEventImageCommandHandler(IRepository<EventImage> imagesRepository, IUnitOfWork unitOfWork, IAzureBlobStorage cloudStorage)
        {
            this.imagesRepository = imagesRepository;
            this.unitOfWork = unitOfWork;
            this.azureBlobStorage = cloudStorage;
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
            await this.unitOfWork.CompleteAsync();

            return Result<string>.Success(imageUrl);
        }
    }
}
