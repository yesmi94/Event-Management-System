// <copyright file="CancelRegistrationCommandHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.EventRegistrations.CancelRegistration
{
    using EventManagementSystem.Application.Interfaces;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Domain.Entities;
    using MediatR;

    public class CancelRegistrationCommandHandler : IRequestHandler<CancelRegistrationCommand, Result<string>>
    {
        private readonly IRepository<EventRegistration> registrationRepository;
        private readonly IUnitOfWork unitOfWork;

        public CancelRegistrationCommandHandler(IRepository<EventRegistration> registrationRepository, IUnitOfWork unitOfWork)
        {
            this.registrationRepository = registrationRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CancelRegistrationCommand request, CancellationToken cancellationToken)
        {
            var eventRegistration = await this.registrationRepository.GetByIdAsync(request.registrationId);

            if (eventRegistration == null)
            {
                return Result<string>.Failure("Failed: Trying to cancel an invalid registration");
            }

            await this.registrationRepository.DeleteAsync(eventRegistration);
            await this.unitOfWork.CompleteAsync();
            return Result<string>.Success($"Successfully Cancelled Registration for Event: {eventRegistration.RegisteredUserName!}");
        }
    }
}
