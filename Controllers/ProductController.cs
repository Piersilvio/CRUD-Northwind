using DBLayer.DAO.IRepository;
using DBLayer.Models;
using DBLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind;
using ServiceLayer.DTO;
using ServiceLayer.IService;
using ServiceLayer.Service.ServiceImp;

namespace Northwind_def.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _serviceProduct;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, IProductService daoProduct)
        {
            _logger = logger;
            _serviceProduct = daoProduct;
        }

        [HttpGet("GetProductsById")]
        public async Task<ActionResult<ProductDTO>> GetProductsById(int id)
        {
            try
            {
                if (!await _serviceProduct.ExistsProduct(id))
                {
                    ModelState.AddModelError("ProductID", "Product doesn't exist");
                    return StatusCode((int)HTTPStatusCode.Conflict, "Product doesn't exist");
                }
                else
                {
                    var em = await _serviceProduct.GetProduct(id);
                    return StatusCode((int)HTTPStatusCode.OK, em);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HTTPStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetProductByNamer")]
        public async Task<ActionResult<List<ProductDTO>>> GetProductsByNamer(string name)
        {
            try
            {
                var emp = await _serviceProduct.GetProductByName(name);
                if (emp is null)
                {
                    return StatusCode((int)HTTPStatusCode.NotFound, "Products not founjd");
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

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductDTO entity)
        {
            try
            {
                if (await _serviceProduct.ExistsProduct(entity.ProductID))
                {
                    ModelState.AddModelError("ProductID", "Product already exist");
                    return StatusCode((int)HTTPStatusCode.Conflict, "Product already exist");
                }
                else
                {
                    var query = await _serviceProduct.CreateProduct(entity);
                    return StatusCode((int)HTTPStatusCode.OK, query);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HTTPStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("UpdateProducts")]
        public async Task<IActionResult> UpdateProducts(ProductDTO p)
        {
            try
            {
                if (!await _serviceProduct.ExistsProduct(p.ProductID))
                {
                    return StatusCode((int)HTTPStatusCode.NotFound, "Product not found");
                }
                else
                {
                    var emp = await _serviceProduct.UpdateProduct(p);

                    return StatusCode((int)HTTPStatusCode.OK, emp);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HTTPStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("DeleteProducts")]
        public async Task<IActionResult> DeleteProducts(ProductDTO entity)
        {
            try
            {
                if (!await _serviceProduct.ExistsProduct(entity.ProductID))
                {
                    return StatusCode((int)HTTPStatusCode.NotFound, "Product Not Found");
                }
                else
                {
                    var flag = await _serviceProduct.DeleteProduct(entity);

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
