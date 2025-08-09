// <copyright file="RegistrationMappingProfile.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.Mapping
{
    using AutoMapper;
    using EventManagementSystem.Application.DTOs.RegistrationDtos;
    using EventManagementSystem.Domain.Entities;

    public class RegistrationMappingProfile : Profile
    {
        public RegistrationMappingProfile()
        {
            this.CreateMap<NewRegistrationDto, GetRegistrationDto>().ReverseMap();
            this.CreateMap<NewRegistrationDto, EventRegistration>();
            this.CreateMap<EventRegistration, GetRegistrationDto>();
        }
    }
}
