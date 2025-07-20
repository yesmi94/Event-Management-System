// <copyright file="CreateEventCommandValidator.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.CreateEvent
{
    using FluentValidation;

    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator()
        {
            this.RuleFor(evt => evt.newEventDto.Title)
                .NotEmpty().WithMessage("Title of the Event is required");

            this.RuleFor(evt => evt.newEventDto.Type)
                .NotEmpty().WithMessage("Type of the Event is required");

            this.RuleFor(evt => evt.newEventDto.EventDate)
                .NotEmpty().WithMessage("Date of the Event is required")
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Event date cannot be in the past");

            this.RuleFor(evt => evt.newEventDto.EventTime)
                .NotEmpty().WithMessage("Time of the Event is required");

            this.RuleFor(evt => evt.newEventDto)
                .NotEmpty().WithMessage("Cutoff date for the registrations of the Event is required")
                .Must(evt => evt.CutoffDate < evt.EventDate)
                .WithMessage("Cutoff date must be before the event date");

            this.RuleFor(evt => evt.newEventDto.Capacity)
                .NotEmpty().WithMessage("Capacity of the Event is required")
                .GreaterThan(0)
                .WithMessage("Capacity must be greater than 0");

            this.RuleFor(evt => evt.newEventDto.Organization)
                .NotEmpty().WithMessage("Organization of the Event is required");

            this.RuleFor(evt => evt.newEventDto.Location)
                .NotEmpty().WithMessage("Location of the Event is required");

            this.RuleFor(evt => evt.newEventDto.Description)
                .NotEmpty().WithMessage("Description of the Event is required");
        }
    }
}
