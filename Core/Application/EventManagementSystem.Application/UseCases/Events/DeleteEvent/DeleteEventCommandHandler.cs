// <copyright file="DeleteEventCommandHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.DeleteEvent
{
    using EventManagementSystem.Application.Interfaces;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Domain.Entities;
    using MediatR;

    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Result<string>>
    {
        private readonly IRepository<Event> eventsRepository;
        private readonly IUnitOfWork unitOfWork;

        public DeleteEventCommandHandler(IRepository<Event> eventsRepository, IUnitOfWork unitOfWork)
        {
            this.eventsRepository = eventsRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var eventToDelete = await this.eventsRepository.GetByIdAsync(request.eventId);

            if (eventToDelete == null)
            {
                return Result<string>.Failure("Failed: Trying to delete an invalid event");
            }

            await this.eventsRepository.DeleteAsync(eventToDelete);
            await this.unitOfWork.CompleteAsync();

            return Result<string>.Success($"Successfully Deleted Event: {eventToDelete.Title}");
        }
    }
}
