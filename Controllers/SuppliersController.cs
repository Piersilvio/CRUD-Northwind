
using DBLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind;
using ServiceLayer.DTO;
using ServiceLayer.IService;

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
                    return StatusCode((int)HTTPStatusCode.Conflict, "Supplier doesn't exist");
                }
                else
                {
                    var em = await _supplierService.GetSupplier(id);
                    return StatusCode((int)HTTPStatusCode.OK, em);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HTTPStatusCode.InternalServerError, ex.Message);
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
                    return StatusCode((int)HTTPStatusCode.NotFound, "Supplier not founjd");
                }
                else
                {
                    return StatusCode((int)HTTPStatusCode.OK, emp);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HTTPStatusCode.InternalServerError, ex.Message);
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
                    return StatusCode((int)HTTPStatusCode.Conflict, "Supplier already exist");
                }
                else
                {
                    var query = await _supplierService.CreateSupplier(entity);
                    return StatusCode((int)HTTPStatusCode.OK, query);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HTTPStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("UpdateSupplier")]
        public async Task<IActionResult> UpdateSupplier(SupplierDTO s)
        {
            try
            {
                if (await _supplierService.ExistsSupplier(s.SupplierID))
                {
                    return StatusCode((int)HTTPStatusCode.NotFound, "Supplier not found");
                }
                else
                {
                    var emp = await _supplierService.UpdateSupplier(s);

                    return StatusCode((int)HTTPStatusCode.OK, emp);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HTTPStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("DeleteSupplier")]
        public async Task<IActionResult> DeleteSupplier(SupplierDTO entity)
        {
            try
            {
                if (!await _supplierService.ExistsSupplier(entity.SupplierID))
                {
                    return StatusCode((int)HTTPStatusCode.NotFound, "Supplier Not Found");
                }
                else
                {
                    var flag = await _supplierService.DeleteSupplier(entity);

                    return StatusCode((int)HTTPStatusCode.OK, flag);
                }
            }
            catch (Exception e)
            {
                return StatusCode((int)HTTPStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
