// <copyright file="UpdateEventCommandValidator.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.UpdateEvent
{
    using FluentValidation;

    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator()
        {
            this.RuleFor(evt => evt.updateEventDto.Title)
                .NotEmpty().WithMessage("Title of the Event is required");

            this.RuleFor(evt => evt.updateEventDto.Type)
                .NotEmpty().WithMessage("Type of the Event is required");

            this.RuleFor(evt => evt.updateEventDto.EventDate)
                .NotEmpty().WithMessage("Date of the Event is required")
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Event date cannot be in the past");

            this.RuleFor(evt => evt.updateEventDto.EventTime)
                .NotEmpty().WithMessage("Time of the Event is required");

            this.RuleFor(evt => evt.updateEventDto)
                .NotEmpty().WithMessage("Cutoff date for the registrations of the Event is required")
                .Must(evt => evt.CutoffDate < evt.EventDate)
                .WithMessage("Cutoff date must be before the event date");

            this.RuleFor(evt => evt.updateEventDto.Capacity)
                .NotEmpty().WithMessage("Capacity of the Event is required")
                .GreaterThan(0)
                .WithMessage("Capacity must be greater than 0");

            this.RuleFor(evt => evt.updateEventDto.Organization)
                .NotEmpty().WithMessage("Organization of the Event is required");

            this.RuleFor(evt => evt.updateEventDto.Location)
                .NotEmpty().WithMessage("Location of the Event is required");

            this.RuleFor(evt => evt.updateEventDto.Description)
                .NotEmpty().WithMessage("Description of the Event is required");
        }
    }
}
