// <copyright file="KeycloakRoleClaimsTransformation.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Persistance
{
    using System.Security.Claims;
    using System.Text.Json;
    using Microsoft.AspNetCore.Authentication;

    public class KeycloakRoleClaimsTransformation : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity is ClaimsIdentity identity)
            {
                var realmAccess = identity.FindFirst("realm_access");

                if (realmAccess != null)
                {
                    using var doc = JsonDocument.Parse(realmAccess.Value);
                    if (doc.RootElement.TryGetProperty("roles", out var rolesElement))
                    {
                        foreach (var role in rolesElement.EnumerateArray())
                        {
                            var roleValue = role.GetString();
                            if (!string.IsNullOrWhiteSpace(roleValue))
                            {
                                identity.AddClaim(new Claim(ClaimTypes.Role, roleValue));
                            }
                        }
                    }
                }
            }

            return Task.FromResult(principal);
        }
    }
}
