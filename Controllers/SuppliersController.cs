using DBLayer.DAO.IRepository;
using DBLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTO;
using ServiceLayer.IService;
using ServiceLayer.Service.ServiceImp;
using System.Data;

namespace Northwind_def.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly ILogger<SupplierController> _logger;

        public SupplierController(ILogger<SupplierController> logger, ISupplierService daoSup)
        {
            _logger = logger;
            _supplierService = daoSup;
        }

        [HttpGet("GetSuppliersById")]
        public async Task<ActionResult<SupplierDTO>> GetSuppliersById(int id)
        {
            try
            {
                if (!await _supplierService.ExistsSupplier(id))
                {
                    ModelState.AddModelError("SupplierID", "Supplier doesn't exist");
                    return StatusCode(409, "Supplier doesn't exist");
                }
                else
                {
                    var em = await _supplierService.GetSupplier(id);
                    return StatusCode(200, em);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetSuppliersByCity")]
        public async Task<ActionResult<List<SupplierDTO>>> GetSuppliersByCity(string city)
        {
            try
            {
                var emp = await _supplierService.GetSupplierByCity(city);
                if (emp is null)
                {
                    return StatusCode(404, "Supplier not founjd");
                }
                else
                {
                    return StatusCode(200, emp);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("CreateSupplier")]
        public async Task<IActionResult> CreateSupplier(SupplierDTO entity)
        {
            try
            {
                if (!await _supplierService.ExistsSupplier(entity.SupplierID))
                {
                    ModelState.AddModelError("SupplierId", "Supplier already exist");
                    return StatusCode(409, "Supplier already exist");
                }
                else
                {
                    var query = await _supplierService.CreateSupplier(entity);
                    return StatusCode(200, query);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateSupplier")]
        public async Task<IActionResult> UpdateSupplier(SupplierDTO s)
        {
            try
            {
                if (await _supplierService.ExistsSupplier(s.SupplierID))
                {
                    return StatusCode(404, "Supplier not found");
                }
                else
                {
                    var emp = await _supplierService.UpdateSupplier(s);

                    return StatusCode(200, emp);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteSupplier")]
        public async Task<IActionResult> DeleteSupplier(SupplierDTO entity)
        {
            try
            {
                if (!await _supplierService.ExistsSupplier(entity.SupplierID))
                {
                    return StatusCode(404, "Supplier Not Found");
                }
                else
                {
                    var flag = await _supplierService.DeleteSupplier(entity);

                    return StatusCode(200, flag);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
