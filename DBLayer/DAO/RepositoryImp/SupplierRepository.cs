
using DBLayer.DAO.IRepository;
using DBLayer.ExceptionHandler;
using DBLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DBLayer.DAO.DAOImpl
{
    public class SupplierRepository : Repository<Supplier>, IRepositorySupplier
    {
        private readonly NorthwindDefContext context;

        public SupplierRepository(NorthwindDefContext dbContext) : base(dbContext)
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

        public async Task<bool> Exists(int id)
            => await context.Supplier.AnyAsync(o => o.SupplierID == id);
    }
}
