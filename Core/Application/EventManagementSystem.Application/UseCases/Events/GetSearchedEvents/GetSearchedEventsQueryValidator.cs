// <copyright file="GetSearchedEventsQueryValidator.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.GetSearchedEvents
{
    using FluentValidation;

    public class GetSearchedEventsQueryValidator : AbstractValidator<GetSearchedEventsQuery>
    {
        public GetSearchedEventsQueryValidator()
        {
            this.RuleFor(x => x.page).GreaterThan(0);

            this.RuleFor(x => x.pageSize).GreaterThan(0).LessThanOrEqualTo(100);

            this.RuleFor(x => x.search).NotEmpty().MaximumLength(100);
        }
    }
}
