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
                        .WithOrigins("https://yellow-water-0d8625800.1.azurestaticapps.net")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            builder.Host.UseSerilog();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration["Authentication:Authority"];
                options.RequireHttpsMetadata = builder.Configuration.GetValue<bool>("Authentication:RequireHttpsMetadata");
                options.Audience = builder.Configuration["Authentication:ClientId"];

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "preferred_username",
                    RoleClaimType = ClaimTypes.Role,
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsync("{\"error\":\"Unauthorized\"}");
                    },
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

            var app = builder.Build();

            app.UseCors();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapOpenApi();
            }

            /*Global Exception Handler*/
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // Configure the HTTP request pipeline.
            app.RegisterAllEndpointGroups();

            app.Run();
        }
    }
}
