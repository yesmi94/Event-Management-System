// <copyright file="Repository.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Persistance.Repositories
{
    using System.Linq.Expressions;
    using EventManagementSystem.Application.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<T> entities;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            this.entities = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(object id) => await this.entities.FindAsync(id);

        public async Task<List<T>> GetAllAsync() => await this.entities.ToListAsync();

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
            await this.entities.Where(predicate).ToListAsync();

        public async Task AddAsync(T entity) => await this.entities.AddAsync(entity);

        public Task DeleteAsync(T entity)
        {
            this.entities.Remove(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(T entity)
        {
            this.entities.Update(entity);
            return Task.CompletedTask;
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return predicate == null
                ? await this.entities.CountAsync()
                : await this.entities.CountAsync(predicate);
        }

        public IQueryable<T> GetQueryable()
        {
            return this.context.Set<T>().AsQueryable();
        }
    }
}
