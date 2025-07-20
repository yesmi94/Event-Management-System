// <copyright file="UserMappingProfile.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.Mapping
{
    using AutoMapper;
    using EventManagementSystem.Application.DTOs.UserDtos;
    using EventManagementSystem.Domain.Entities;

    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            this.CreateMap<NewUserDto, GetUserDto>();
            this.CreateMap<GetUserDto, NewUserDto>();
            this.CreateMap<NewUserDto, User>();
            this.CreateMap<User, GetUserDto>();
        }
    }
}
