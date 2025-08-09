// <copyright file="UploadEventImageCommandValidator.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.UploadEventImage
{
    using FluentValidation;
    using Microsoft.AspNetCore.Http;

    public class UploadEventImageCommandValidator : AbstractValidator<UploadEventImageCommand>
    {
        private const long MaxFileSizeInBytes = 5 * 1024 * 1024;
        private readonly string[] allowedContentTypes = new[] { "image/jpeg", "image/png", "image/jpg" };

        public UploadEventImageCommandValidator()
        {
            this.RuleFor(x => x.eventId)
            .NotEmpty().WithMessage("Event ID is required.");

            this.RuleFor(x => x.imageFile)
                .NotNull().WithMessage("Image file is required.")
                .Must(this.BeAValidContentType).WithMessage("Only JPG or PNG images are allowed.");
        }

        private bool BeAValidContentType(IFormFile file)
        {
            return file != null && this.allowedContentTypes.Contains(file.ContentType);
        }

        private bool BeWithinSizeLimit(IFormFile file)
        {
            return file != null && file.Length <= MaxFileSizeInBytes;
        }
    }
}
