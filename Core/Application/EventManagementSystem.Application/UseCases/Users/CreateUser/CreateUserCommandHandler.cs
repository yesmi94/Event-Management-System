// <copyright file="CreateUserCommandHandler.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.UseCases.Users.CreateUser
{
    using AutoMapper;
    using AutoMapper.Execution;
    using EventManagementSystem.Application.DTOs.UserDtos;
    using EventManagementSystem.Application.Interfaces;
    using EventManagementSystem.Application.Patterns;
    using EventManagementSystem.Domain.Entities;
    using MediatR;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<GetUserDto>>
    {
        private readonly IRepository<User> userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateUserCommandHandler(IRepository<User> userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<GetUserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userDto = request.newUserDto;

            User user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                UserType = userDto.UserType,
            };

            var getUserDto = this.mapper.Map<GetUserDto>(user);
            await this.userRepository.AddAsync(user);
            await this.unitOfWork.CompleteAsync();

            return Result<GetUserDto>.Success(getUserDto);
        }
    }
}
