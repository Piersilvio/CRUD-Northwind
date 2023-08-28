using DBLayer.ExceptionHandler;
using DBLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.DAO.IRepository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected NorthwindDefContext DbContextRepo { get; set; }

        public Repository(NorthwindDefContext dbContextRepo)
        {
            DbContextRepo = dbContextRepo;
        }

        public async Task<T> Create(T entity)
        {
            return await ExceptionHandlerClass.HandleExceptions<T>(async () =>
            {
                DbContextRepo.Set<T>().Add(entity);
                await DbContextRepo.SaveChangesAsync();
                return entity;
            });
        }

        public async Task<T> Update(T entity)
        {
            return await ExceptionHandlerClass.HandleExceptions<T>(async () =>
            {
                DbContextRepo.Set<T>().Update(entity);
                await DbContextRepo.SaveChangesAsync();
                return entity;
            });
        }

        public async Task<bool> Delete(T entity)
        {
            return await ExceptionHandlerClass.HandleExceptions<bool>(async () =>
            {
                bool isDeleted = false;
                DbContextRepo.Set<T>().Remove(entity);
                if (await DbContextRepo.SaveChangesAsync() > 0)
                {
                    isDeleted = true;
                }

                return isDeleted;
            });
        }

        public async Task<T> Get(int id)
        {
            return await ExceptionHandlerClass.HandleExceptions<T>(async () =>
            {
                return await DbContextRepo.Set<T>().FindAsync(id);
            });
        }
    }
}
