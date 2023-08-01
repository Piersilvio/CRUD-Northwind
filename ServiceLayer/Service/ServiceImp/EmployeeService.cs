using DBLayer.DAO.IRepository;
using DBLayer.Models;
using ServiceLayer.AutoMapper;
using ServiceLayer.DTO;
using ServiceLayer.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service.ServiceImp
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryEmployee employeeRepository;
        private readonly IMapperConfig mapperConfig;

        public EmployeeService(IRepositoryEmployee employeeRepository, IMapperConfig mapperConfig)
        {
            this.employeeRepository = employeeRepository;
            this.mapperConfig = mapperConfig;
        }

        public async Task<EmployeeDTO> CreateEmployee(EmployeeDTO employeeDTO)
        {
            var map = mapperConfig.InitializeAutomapper();

            var employeeToSend = map.Map<Employee>(employeeDTO);
            var createEmp = await employeeRepository.Create(employeeToSend);
            var employeeToSave = map.Map<EmployeeDTO>(createEmp);

            return employeeToSave;
        }

        public async Task<bool> DeleteEmployee(EmployeeDTO employeeDTO)
        {
            var map = mapperConfig.InitializeAutomapper();
            var employeeToSend = map.Map<Employee>(employeeDTO);
            var deleteEmp = await employeeRepository.Delete(employeeToSend);

            return deleteEmp;
        }

        public async Task<bool> ExistsEmployee(int id)
        {
            var map = mapperConfig.InitializeAutomapper();

            var existEmp = await employeeRepository.Exists(id);
            return existEmp;
        }

        public async Task<EmployeeDTO> GetEmployee(int id)
        {
            var map = mapperConfig.InitializeAutomapper();

            var byIdEmp = await employeeRepository.Get(id);
            var empToSave = map.Map<EmployeeDTO>(byIdEmp);
            return empToSave;
        }

        public async Task<List<EmployeeDTO>> GetEmployeeByCity(string city)
        {
            var map = mapperConfig.InitializeAutomapper();

            var empList = await employeeRepository.GetEmpByCity(city);
            var empListToSave = map.Map<List<EmployeeDTO>>(empList);

            return empListToSave;
        }

        public async Task<EmployeeDTO> UpdateEmployee(EmployeeDTO employeeDTO)
        {
            var map = mapperConfig.InitializeAutomapper();

            var empToSend = map.Map<Employee>(employeeDTO);

            ///if(emp)
            empToSend.EmployeeID = employeeDTO.EmployeeID;
            empToSend.FirstName = employeeDTO.FirstName;
            empToSend.LastName = employeeDTO.LastName;
            empToSend.Title = employeeDTO.Title;
            empToSend.TitleOfCourtesy = employeeDTO.TitleOfCourtesy;
            empToSend.BirthDate = employeeDTO.BirthDate;
            empToSend.HireDate = employeeDTO.HireDate;
            empToSend.Address = employeeDTO.Address;
            empToSend.City = employeeDTO.City;
            empToSend.Region = employeeDTO.Region;
            empToSend.PostalCode = employeeDTO.PostalCode;
            empToSend.Country = employeeDTO.Country;
            empToSend.HomePhone = employeeDTO.HomePhone;
            empToSend.Extension = employeeDTO.Extension;
            empToSend.Notes = employeeDTO.Notes;
            empToSend.ReportsTo = employeeDTO.ReportsTo;
            empToSend.PhotoPath = employeeDTO.PhotoPath;

            var updateEmp = await employeeRepository.Update(empToSend);
            var empToSave = map.Map<EmployeeDTO>(updateEmp);

            return empToSave;
        }
    }
}
