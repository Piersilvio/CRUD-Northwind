using DBLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.DAO.IRepository
{
    public interface IRepositoryEmployee : IRepository<Employee>
    {
        Task<List<Employee>> GetEmpByCity(string city);
        Task<bool> Exists(int employeeId);
    }
}
