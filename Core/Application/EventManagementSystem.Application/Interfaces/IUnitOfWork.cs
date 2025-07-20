// <copyright file="IUnitOfWork.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
    }
}
