// <copyright file="GetUsersQueryHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Users.GetUsers
{
    using AutoMapper;
    using EventManagementSystem.Application.DTOs.UserDtos;
    using EventManagementSystem.Application.Interfaces;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Domain.Entities;
    using MediatR;

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<List<GetUserDto>>>
    {
        private readonly IRepository<User> usersRepository;
        private readonly IMapper mapper;

        public GetUsersQueryHandler(IRepository<User> usersRepository, IMapper mapper)
        {
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }

        public async Task<Result<List<GetUserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var allUsers = await this.usersRepository.GetAllAsync();

            var usersList = this.mapper.Map<List<GetUserDto>>(allUsers);

            return Result<List<GetUserDto>>.Success(usersList);
        }
    }
}
