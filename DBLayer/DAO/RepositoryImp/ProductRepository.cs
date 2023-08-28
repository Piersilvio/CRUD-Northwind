
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
    public class ProductRepository : Repository<Product>, IRepositoryProduct
    {
        private readonly NorthwindDefContext context;

        public ProductRepository(NorthwindDefContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }

        public async Task<List<Product>> GetProductByName(string name)
        {
            return await ExceptionHandlerClass.HandleExceptions<List<Product>>(async () =>
            {
                return await context.Product.Where(e => e.ProductName == name).ToListAsync();
            });
        } 

        public async Task<bool> Exists(int ProductID)
            => await context.Product.AnyAsync(o =>  o.ProductID == ProductID);
    }
}
