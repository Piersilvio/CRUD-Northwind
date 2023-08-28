
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
    public class OrderrRepository : Repository<Orderr>, IRepositoryOrderr
    {
        private readonly NorthwindDefContext context;

        public OrderrRepository(NorthwindDefContext dbContext) : base(dbContext)
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

        public async Task<bool> Exists(int OrderID)
            => await context.Orderr.AnyAsync(o => o.OrderID == OrderID);
    }
}
