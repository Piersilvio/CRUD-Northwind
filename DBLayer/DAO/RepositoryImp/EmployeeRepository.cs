using DBLayer.DAO.IRepository;
using DBLayer.ExceptionHandler;
using DBLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DBLayer.DAO.DAOImpl
{
    public class EmployeeRepository : IRepositoryEmployee
    {
        private readonly NorthwindDefContext context;

        //iniettato per DI 
        public EmployeeRepository(NorthwindDefContext dbContext)
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
        
        public async Task<Employee> Create(Employee entity)
        {
            return await ExceptionHandlerClass.HandleExceptions<Employee>(async () =>
            {
                context.Employee.Add(entity);
                await context.SaveChangesAsync();
                return entity;
            });
        }

        public async Task<Employee> Get(int id)
        {
            return await ExceptionHandlerClass.HandleExceptions<Employee>(async () =>
            {
                Employee? employees = await context.Employee.Where(e => e.EmployeeID == id)
                .FirstOrDefaultAsync(e => e.EmployeeID == id);
                return employees;
            });
        }

        public async Task<Employee> Update(Employee employees)
        {
            return await ExceptionHandlerClass.HandleExceptions<Employee>(async () =>
            {
                context.Employee.Update(employees);
                await context.SaveChangesAsync();
                return employees;
            });
        }

        public async Task<bool> Delete(Employee employees)
        {
            return await ExceptionHandlerClass.HandleExceptions<bool>(async () =>
            {
                bool isDeleted = false;
                context.Employee.Remove(employees);
                if (await context.SaveChangesAsync() > 0)
                {
                    isDeleted = true;
                }

                return isDeleted;
            });
        }

        public async Task<bool> Exists(int employeeId)
            => await context.Employee.AnyAsync(o =>  o.EmployeeID == employeeId);
    }
}
