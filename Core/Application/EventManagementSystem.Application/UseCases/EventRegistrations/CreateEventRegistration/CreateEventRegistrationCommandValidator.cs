// <copyright file="CreateEventRegistrationCommandValidator.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.EventRegistrations.CreateEventRegistration
{
    using FluentValidation;

    public class CreateEventRegistrationCommandValidator : AbstractValidator<CreateEventRegistrationCommand>
    {
        public CreateEventRegistrationCommandValidator()
        {
            this.RuleFor(registration => registration.newRegistrationDto.RegisteredUserName)
                .NotEmpty().WithMessage("Name of the registered user is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters")
                .Matches("^[a-zA-Z ]+$").WithMessage("Name must contain only letters and spaces");

            this.RuleFor(registration => registration.newRegistrationDto.Email)
                .NotEmpty().WithMessage("Email of the registered user is required")
                .EmailAddress().WithMessage("A valid email address is required")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

            this.RuleFor(registration => registration.newRegistrationDto.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number of the registered user is required")
                .Matches(@"^07\d{8}$").WithMessage("Phone number must be valid");
        }
    }
}
