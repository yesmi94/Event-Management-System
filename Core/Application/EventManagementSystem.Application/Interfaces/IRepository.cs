// <copyright file="IRepository.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Application.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<T>
        where T : class
    {
        Task<T?> GetByIdAsync(object id);

        Task<List<T>> GetAllAsync();

        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);

        Task DeleteAsync(T entity);

        Task UpdateAsync(T entity);
    }
}
