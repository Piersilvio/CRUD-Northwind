
using DBLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind;
using ServiceLayer.DTO;
using ServiceLayer.IService;
using System.Net;

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
                    return StatusCode((int)HttpStatusCode.Conflict, "Supplier doesn't exist");
                }
                else
                {
                    var em = await _supplierService.GetSupplier(id);
                    return StatusCode((int)HttpStatusCode.OK, em);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
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
                    return StatusCode((int)HttpStatusCode.NotFound, "Supplier not founjd");
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, emp);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
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
                    return StatusCode((int)HttpStatusCode.Conflict, "Supplier already exist");
                }
                else
                {
                    var query = await _supplierService.CreateSupplier(entity);
                    return StatusCode((int)HttpStatusCode.OK, query);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("UpdateSupplier")]
        public async Task<IActionResult> UpdateSupplier(SupplierDTO s)
        {
            try
            {
                if (await _supplierService.ExistsSupplier(s.SupplierID))
                {
                    return StatusCode((int)HttpStatusCode.NotFound, "Supplier not found");
                }
                else
                {
                    var emp = await _supplierService.UpdateSupplier(s);

                    return StatusCode((int)HttpStatusCode.OK, emp);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("DeleteSupplier")]
        public async Task<IActionResult> DeleteSupplier(SupplierDTO entity)
        {
            try
            {
                if (!await _supplierService.ExistsSupplier(entity.SupplierID))
                {
                    return StatusCode((int)HttpStatusCode.NotFound, "Supplier Not Found");
                }
                else
                {
                    var flag = await _supplierService.DeleteSupplier(entity);

                    return StatusCode((int)HttpStatusCode.OK, flag);
                }
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
