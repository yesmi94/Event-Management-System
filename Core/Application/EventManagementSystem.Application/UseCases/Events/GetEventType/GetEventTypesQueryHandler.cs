// <copyright file="GetEventTypesQueryHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Events.GetEventType
{
    using EventManagementSystem.Application.DTOs.EventDtos;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Domain.Enums;
    using MediatR;

    public class GetEventTypesQueryHandler : IRequestHandler<GetEventTypesQuery, Result<List<EventTypeDto>>>
    {
        public Task<Result<List<EventTypeDto>>> Handle(GetEventTypesQuery request, CancellationToken cancellationToken)
        {
            var enumValues = Enum.GetValues(typeof(EventType))
                .Cast<EventType>()
                .Select(e => new EventTypeDto
                {
                    Value = (int)e,
                    Label = SplitPascalCase(e.ToString()),
                })
                .ToList();

            return Task.FromResult(Result<List<EventTypeDto>>.Success(enumValues));
        }

        private static string SplitPascalCase(string input)
        {
            return System.Text.RegularExpressions.Regex
                .Replace(input, "(\\B[A-Z])", " $1");
        }
    }
}
