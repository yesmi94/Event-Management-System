// <copyright file="EventMappingProfile.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.Mapping
{
    using AutoMapper;
    using EventManagementSystem.Application.DTOs.EventDtos;
    using EventManagementSystem.Domain.Entities;

    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            this.CreateMap<GetEventDto, NewEventDto>().ReverseMap();
            this.CreateMap<Event, GetEventDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
            this.CreateMap<GetEventDto, Event>().ReverseMap();
            this.CreateMap<Event, NewEventDto>();
            this.CreateMap<UpdateEventDto, GetEventDto>();
        }
    }
}
