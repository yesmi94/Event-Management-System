// <copyright file="EndpointGroupRegistrationExtension.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.API.Extensions
{
    using System.Reflection;

    public static class EndpointGroupRegistrationExtensions
    {
        public static void RegisterAllEndpointGroups(this IEndpointRouteBuilder app)
        {
            var endpointGroupTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(IEndpointGroup).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            using var scope = app.ServiceProvider.CreateScope();

            foreach (var type in endpointGroupTypes)
            {
                var service = scope.ServiceProvider.GetService(type) as IEndpointGroup;

                if (service is not null)
                {
                    service.MapEndpoints(app);
                }
            }
        }
    }
}
