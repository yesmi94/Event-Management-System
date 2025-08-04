// <copyright file="GetEventsQueryValidator.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.GetEvents
{
    using FluentValidation;

    public class GetEventsQueryValidator : AbstractValidator<GetEventsQuery>
    {
        public GetEventsQueryValidator()
        {
            this.RuleFor(x => x.page)
                .GreaterThan(0).WithMessage("Page must be greater than 0.");

            this.RuleFor(x => x.pageSize)
                .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100.");
        }
    }
}
