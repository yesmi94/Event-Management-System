// <copyright file="GetFilteredEventsQueryValidator.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.GetFilteredEvents
{
    using FluentValidation;

    public class GetFilteredEventsQueryValidator : AbstractValidator<GetFilteredEventsQuery>
    {
        public GetFilteredEventsQueryValidator()
        {
            this.RuleFor(x => x.page)
                .GreaterThan(0).WithMessage("Page must be greater than 0.");

            this.RuleFor(x => x.pageSize)
                .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100.");

            this.When(x => x.dateFrom.HasValue && x.dateTo.HasValue, () =>
            {
                this.RuleFor(x => x)
                    .Must(x => x.dateFrom <= x.dateTo)
                    .WithMessage("DateFrom must be before or equal to DateTo.");
            });
        }
    }
}
