
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
    public class ProductRepository : IRepositoryProduct
    {
        private readonly NorthwindDefContext context;

        //iniettato per DI 
        public ProductRepository(NorthwindDefContext dbContext)
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

        public async Task<Product> Create(Product entity)
        {
            return await ExceptionHandlerClass.HandleExceptions<Product>(async () =>
            {
                context.Product.Add(entity);
                await context.SaveChangesAsync();

                return entity;
            });
        }

        public async Task<Product> Get(int id)
        {
            return await ExceptionHandlerClass.HandleExceptions<Product>(async () =>
            {
                Product? product = await context.Product.Where(e => e.ProductID == id)
                .FirstOrDefaultAsync(e => e.ProductID == id);

                return product;
            });
        }

        public async Task<Product> Update(Product p)
        {
            return await ExceptionHandlerClass.HandleExceptions<Product>(async () =>
            {
                context.Product.Update(p);
                await context.SaveChangesAsync();

                return p;
            });
        }

        public async Task<bool> Delete(Product p)
        {
            return await ExceptionHandlerClass.HandleExceptions<bool>(async () =>
            {
                bool isDeleted = false;

                context.Product.Remove(p);
                if (await context.SaveChangesAsync() > 0)
                {
                    isDeleted = true;
                }

                return isDeleted;
            });
        }

        public async Task<bool> Exists(int ProductID)
            => await context.Product.AnyAsync(o =>  o.ProductID == ProductID);
    }
}
