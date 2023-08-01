
using DBLayer;
using DBLayer.DAO.IRepository;
using DBLayer.ExceptionHandler;
using DBLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.DAO.DAOImpl
{
    public class SupplierRepository : IRepositorySupplier
    {
        private readonly NorthwindDefContext context;

        //iniettato per DI 
        public SupplierRepository(NorthwindDefContext dbContext)
        {
            context = dbContext;
        }

        public async Task<List<Supplier>> GetSupplierByCity(string city)
        {
            return await ExceptionHandlerClass.HandleExceptions<List<Supplier>>(async () =>
            {
                return await context.Supplier.Where(e => e.City == city).ToListAsync();
            });
        }
        
        public async Task<Supplier> Create(Supplier entity)
        {
            return await ExceptionHandlerClass.HandleExceptions<Supplier>(async () =>
            {
                context.Supplier.Add(entity);
                await context.SaveChangesAsync();

                return entity;
            });
        }

        public async Task<Supplier> Get(int id)
        {
            return await ExceptionHandlerClass.HandleExceptions<Supplier>(async () =>
            {
                Supplier? suplier = await context.Supplier.Where(e => e.SupplierID == id)
                .FirstOrDefaultAsync(e => e.SupplierID == id);

                return suplier;
            });
        }

        public async Task<Supplier> Update(Supplier s)
        {
            return await ExceptionHandlerClass.HandleExceptions<Supplier>(async () =>
            {
                context.Supplier.Update(s);
                await context.SaveChangesAsync();

                return s;
            });
        }

        public async Task<bool> Delete(Supplier s)
        {
            return await ExceptionHandlerClass.HandleExceptions<bool>(async () =>
            {
                bool isDeleted = false;

                context.Supplier.Remove(s);
                if (await context.SaveChangesAsync() > 0)
                {
                    isDeleted = true;
                }

                return isDeleted;
            });
        }

        public async Task<bool> Exists(int id)
            => await context.Supplier.AnyAsync(o => o.SupplierID == id);
    }
}
