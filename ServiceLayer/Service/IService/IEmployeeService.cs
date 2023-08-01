using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.IService
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetEmployeeByCity(string city);
        Task<EmployeeDTO> CreateEmployee(EmployeeDTO employeeDTO);
        Task<EmployeeDTO> GetEmployee(int id);
        Task<EmployeeDTO> UpdateEmployee(EmployeeDTO employeeDTO);
        Task<bool> DeleteEmployee(EmployeeDTO employeeDTO);
        Task<bool> ExistsEmployee(int id);
    }
}
