// <copyright file="Program.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.API
{
    using System.Security.Claims;
    using EventManagementSystem.API.Endpoints;
    using EventManagementSystem.API.Extensions;
    using EventManagementSystem.API.Middlewares;
    using EventManagementSystem.Application.Behaviors;
    using EventManagementSystem.Application.Interfaces;
    using EventManagementSystem.Application.Mapping;
    using EventManagementSystem.Application.UseCases.Events.CreateEvent;
    using EventManagementSystem.Persistance;
    using EventManagementSystem.Persistance.Repositories;
    using EventManagementSystem.Utility;
    using FluentValidation;
    using MediatR;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Serilog;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy
                        .WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // Logging
            builder.Host.UseSerilog();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = "http://localhost:8080/realms/event-management-system";
                options.RequireHttpsMetadata = false;
                options.ClientId = "event-system-backend";
                options.ClientSecret = "LujqvYYZ68deq7ftFqsRCdELC3cE7Iil";
                options.ResponseType = "code";

                options.SaveTokens = true;

                options.CallbackPath = "/signin-oidc";

                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "preferred_username",
                    RoleClaimType = ClaimTypes.Role,
                    ValidAudiences = new[] { "event-system-backend", "account" },
                };

                options.GetClaimsFromUserInfoEndpoint = true;
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "http://localhost:8080/realms/event-management-system";
                options.RequireHttpsMetadata = false;
                options.Audience = "event-system-backend";

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "preferred_username",
                    RoleClaimType = ClaimTypes.Role,
                };
            });
            builder.Services.AddTransient<IClaimsTransformation, KeycloakRoleClaimsTransformation>();

            builder.Services.AddSwaggerGen();

            builder.Services.AddOpenApi();
            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateEventCommandHandler).Assembly));

            /*Logging pipeline*/
            builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipeline<,>));

            /*Validation pipeline*/
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));

            builder.Services.AddValidatorsFromAssemblyContaining<CreateEventCommandValidator>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IEventRegistrationRepository, EventRegistrationRepository>();
            builder.Services.AddScoped<IEventsRepository, EventsRepository>();
            builder.Services.AddAutoMapper(typeof(EventMappingProfile).Assembly);
            builder.Services.AddScoped<EventEndpoints>();
            builder.Services.AddScoped<EventRegistrationEndpoints>();
            builder.Services.AddScoped<DashboardEndpoints>();
            builder.Services.AddScoped<UserEndpoints>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddSingleton<IAzureBlobStorage, AzureBlobStorage>();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            SerilogConfiguration.ConfigureSerilog(builder.Host, builder.Configuration);

            // Add services to the container.
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-XSRF-TOKEN";
            });

            var app = builder.Build();

            app.UseCors();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapOpenApi();
            }

            /*app.UseHttpsRedirection();*/

            /*Global Exception Handler*/
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAntiforgery();

            // Configure the HTTP request pipeline.
            app.RegisterAllEndpointGroups();

            app.Run();
        }
    }
}
