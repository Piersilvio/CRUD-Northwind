using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using DBLayer.Models;
using ServiceLayer.DTO;
using ServiceLayer.IService;
using Northwind;
using System.Net;

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
                    //ModelState.AddModelError("EmployeeId", "Employee doesn't exist");
                    return StatusCode((int)HTTPStatusCode.Conflict, "Employee doesn't exist");
                }
                else
                {
                    var employee = await _employeeService.GetEmployee(id);
                    return StatusCode((int)HTTPStatusCode.OK, employee);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HTTPStatusCode.InternalServerError, ex.StackTrace);
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
                    return StatusCode((int)HTTPStatusCode.NotFound, "Employee not founjd");
                }
                else
                {
                    return StatusCode((int)HTTPStatusCode.OK, emp);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HTTPStatusCode.InternalServerError, ex.StackTrace);
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
                    return StatusCode((int)HTTPStatusCode.Conflict, "Employee already exist");
                }
                else
                {
                    var query = await _employeeService.CreateEmployee(entity);
                    return StatusCode((int)HTTPStatusCode.OK, query);
                }
            }catch (Exception ex)
            {
                return StatusCode((int)HTTPStatusCode.InternalServerError, ex.StackTrace);
            }
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeDTO em)
        {
            try
            {
                if (!await _employeeService.ExistsEmployee(em.EmployeeID))
                {
                    return StatusCode((int)HTTPStatusCode.NotFound, "Employee not found");
                }
                else
                {
                    var emp = await _employeeService.UpdateEmployee(em);
                    return StatusCode((int)HTTPStatusCode.OK, emp);
                }
            }catch(Exception ex)
            {
                return StatusCode((int)HTTPStatusCode.InternalServerError, ex.StackTrace);
            }
        }

        [HttpDelete("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(EmployeeDTO em)
        {
            try
            {
                if (!await _employeeService.ExistsEmployee(em.EmployeeID))
                {
                    return StatusCode((int)HTTPStatusCode.NotFound, "Employee Not Found");
                }
                else
                {
                    var flag = await _employeeService.DeleteEmployee(em);
                    return StatusCode((int)HTTPStatusCode.OK, flag);
                }
            }catch(Exception e)
            {
                return StatusCode((int)HTTPStatusCode.InternalServerError, e.StackTrace);
            }
        }
    }
}
