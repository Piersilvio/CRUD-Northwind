using DBLayer.DAO.IRepository;
using DBLayer.ExceptionHandler;
using DBLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DBLayer.DAO.DAOImpl
{
    public class EmployeeRepository : Repository<Employee>, IRepositoryEmployee
    {
        private readonly NorthwindDefContext context;

        public EmployeeRepository(NorthwindDefContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }

        public async Task<List<Employee>> GetEmpByCity(string city)
        {
            return await ExceptionHandlerClass.HandleExceptions<List<Employee>>(async () =>
            {
                return await context.Employee.Where(e => e.City == city).ToListAsync();
            });
        }

        public async Task<bool> Exists(int employeeId)
            => await context.Employee.AnyAsync(o =>  o.EmployeeID == employeeId);
    }
}
