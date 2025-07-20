// <copyright file="UnitOfWork.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Persistance
{
    using EventManagementSystem.Application.Interfaces;
    using Microsoft.EntityFrameworkCore.Storage;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        private IDbContextTransaction? currentTransaction;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> CompleteAsync()
        {
            try
            {
                this.currentTransaction = await this.context.Database.BeginTransactionAsync();
                var result = await this.context.SaveChangesAsync();
                await this.currentTransaction.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                if (this.currentTransaction == null)
                {
                    await this.currentTransaction!.RollbackAsync();
                }

                throw;
            }
            finally
            {
                if (this.currentTransaction != null)
                {
                    await this.currentTransaction.DisposeAsync();
                    this.currentTransaction = null;
                }
            }
        }
    }
}
