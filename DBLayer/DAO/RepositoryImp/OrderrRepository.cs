
using DBLayer.DAO.IRepository;
using DBLayer.ExceptionHandler;
using DBLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.DAO.DAOImpl
{
    public class OrderrRepository : IRepositoryOrderr
    {
        private readonly NorthwindDefContext context;

        //iniettato per DI 
        public OrderrRepository(NorthwindDefContext dbContext)
        {
            context = dbContext;
        }

        public async Task<List<Orderr>> GetOrdersByCustomerID(string customerID)
        {
            return await ExceptionHandlerClass.HandleExceptions<List<Orderr>>(async () =>
            {
                return await context.Orderr
                        .Join(context.Customer, o => o.CustomerID, c => c.CustomerID, (o, c) => o)
                        .Where(o => o.CustomerID == customerID)
                        .ToListAsync();
            });
        }               

        public async Task<List<Orderr>> GetOrdersByCity(string city)
        {
            return await ExceptionHandlerClass.HandleExceptions<List<Orderr>>(async () =>
            {
                return await context.Orderr
                        .Join(context.Customer, o => o.CustomerID, c => c.CustomerID, (o, c) => new { Order = o, Customer = c })
                        .Where(x => x.Customer.City == city)
                        .Select(x => x.Order)
                        .ToListAsync();
            });
        }
            
        public async Task<Orderr> Create(Orderr entity)
        {
            return await ExceptionHandlerClass.HandleExceptions<Orderr>(async () =>
            {
                context.Orderr.Add(entity);
                await context.SaveChangesAsync();
                return entity;
            });
        }

        public async Task<Orderr> Get(int id)
        {
            return await ExceptionHandlerClass.HandleExceptions<Orderr>(async () =>
            {
                Orderr? order = await context.Orderr.Where(e => e.OrderID == id)
                .FirstOrDefaultAsync(e => e.OrderID == id);
                return order;
            });
        }

        public async Task<Orderr> Update(Orderr entity)
        {
            return await ExceptionHandlerClass.HandleExceptions<Orderr>(async () =>
            {
                context.Orderr.Update(entity);
                await context.SaveChangesAsync();

                return entity;
            });
        }

        public async Task<bool> Delete(Orderr o)
        {
            return await ExceptionHandlerClass.HandleExceptions<bool>(async () =>
            {
                bool isDeleted = false;

                context.Orderr.Remove(o);
                if (await context.SaveChangesAsync() > 0)
                {
                    isDeleted = true;
                }

                return isDeleted;
            });
        }

        public async Task<bool> Exists(int OrderID)
            => await context.Orderr.AnyAsync(o => o.OrderID == OrderID);
    }
}
