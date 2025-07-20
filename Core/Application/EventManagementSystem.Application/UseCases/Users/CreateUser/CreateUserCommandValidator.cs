// <copyright file="CreateUserCommandValidator.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Users.CreateUser
{
    using FluentValidation;

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            this.RuleFor(user => user.newUserDto.Name)
                .NotEmpty().WithMessage("Name of the User is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            this.RuleFor(user => user.newUserDto.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number of the User is required")
                .Matches(@"^\+947\d{8}$").WithMessage("Phone number must be valid");

            this.RuleFor(user => user.newUserDto.Email)
                .NotEmpty().WithMessage("Email of the registered user is required")
                .EmailAddress().WithMessage("A valid email address is required")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

            this.RuleFor(user => user.newUserDto.UserType)
                .NotEmpty().WithMessage("Type of the user is required");
        }
    }
}
