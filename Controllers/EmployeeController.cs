using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using DBLayer.Models;
using DBLayer.DAO.IRepository;
using ServiceLayer.AutoMapper;
using ServiceLayer.DTO;
using ServiceLayer.IService;

namespace Northwind_def.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet("GetEmployeeById")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(int id)
        {
            try
            {
                if (!await _employeeService.ExistsEmployee(id))
                {
                    ModelState.AddModelError("EmployeeId", "Employee doesn't exist");
                    return StatusCode(409, "Employee doesn't exist");
                }
                else
                {
                    var employee = await _employeeService.GetEmployee(id);
                    return StatusCode(200, employee);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.StackTrace);
            }
        }

        [HttpGet("GetEmployeesByCity")]
        public async Task<ActionResult<List<EmployeeDTO>>> GetEmployeesByCity(string city)
        {
            try
            {
                var emp = await _employeeService.GetEmployeeByCity(city);
                if (emp is null)
                {
                    return StatusCode(404, "Employee not founjd");
                }
                else
                {
                    return StatusCode(200, emp);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.StackTrace);
            }
        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(EmployeeDTO entity)
        {
            try
            {
                if (await _employeeService.ExistsEmployee(entity.EmployeeID))
                {
                    ModelState.AddModelError("EmployeeId", "Employee already exist");
                    return StatusCode(409, "Employee already exist");
                }
                else
                {
                    var query = await _employeeService.CreateEmployee(entity);
                    return StatusCode(200, query);
                }
            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeDTO em)
        {
            try
            {
                if (!await _employeeService.ExistsEmployee(em.EmployeeID))
                {
                    return StatusCode(404, "Employee not found");
                }
                else
                {
                    var emp = await _employeeService.UpdateEmployee(em);
                    return StatusCode(200, emp);
                }
            }catch(Exception ex)
            {
                return StatusCode(500, ex.StackTrace);
            }
        }

        [HttpDelete("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(EmployeeDTO em)
        {
            try
            {
                if (!await _employeeService.ExistsEmployee(em.EmployeeID))
                {
                    return StatusCode(404, "Employee Not Found");
                }
                else
                {
                    var flag = await _employeeService.DeleteEmployee(em);
                    return StatusCode(200, flag);
                }
            }catch(Exception e)
            {
                return StatusCode(500, e.StackTrace);
            }
        }
    }
}
