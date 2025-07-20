// <copyright file="Program.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.API
{
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
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Logging
            builder.Host.UseSerilog();

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
            builder.Services.AddAutoMapper(typeof(EventMappingProfile).Assembly);
            builder.Services.AddScoped<EventEndpoints>();
            builder.Services.AddScoped<EventRegistrationEndpoints>();
            builder.Services.AddScoped<UserEndpoints>();
            builder.Services.AddScoped<AuthEndpoints>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddDbContext<ApplicationDbContext>(options => options
            .UseLazyLoadingProxies()
            .UseSqlServer(
                "Server=.\\SQLEXPRESS;Database=EventManagementDatabase;Trusted_Connection=True;TrustServerCertificate=True;",
                sqlOptions => sqlOptions.MigrationsAssembly("EventManagementSystem.Persistance")));

            SerilogConfiguration.ConfigureSerilog(builder.Host, builder.Configuration);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.RegisterAllEndpointGroups();

            // Configure the HTTP request pipeline.
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

            app.UseAuthorization();

            app.Run();
        }
    }
}
