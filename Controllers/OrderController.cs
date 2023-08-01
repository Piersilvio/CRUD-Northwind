using DBLayer.DAO.IRepository;
using DBLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.AutoMapper;
using ServiceLayer.DTO;
using ServiceLayer.IService;
using ServiceLayer.Service.ServiceImp;

namespace Northwind_def.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = UserRoles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderrService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger, IOrderrService service)
        {
            _logger = logger;
            _orderService = service;
        }

        [HttpGet("GetOrderById")]
        public async Task<ActionResult<OrderrDTO>> GetOrderById(int id)
        {
            try
            {
                if (!await _orderService.ExistsOrder(id))
                {
                    return StatusCode(404, "order not found");
                }
                else
                {
                    var orderById = await _orderService.GetOrder(id);
                    return StatusCode(200, orderById);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.StackTrace);
            }
        }

        [HttpGet("GetOrderByCustomerID")]
        public async Task<ActionResult<List<OrderrDTO>>> GetOrdersByCustomerID(string customerID)
        {
            try
            {
                var e = await _orderService.GetOrdersByCustomerID(customerID);
                if (e is null)
                {
                    return StatusCode(404, "Orders not found");
                }
                else
                {
                    return StatusCode(200, e);
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, e.StackTrace);
            }
        }

        [HttpGet("GetOrdersByCity")]
        public async Task<ActionResult<List<OrderrDTO>>> GetOrdersByCity(string city)
        {
            try
            {
                var e = await _orderService.GetOrderByCity(city);
                if (e is null)
                {
                    return StatusCode(404, "Orders not found");
                }
                else
                {
                    return StatusCode(200, e);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.StackTrace);
            }
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(OrderrDTO entity)
        {
            try
            {
                if (await _orderService.ExistsOrder(entity.OrderID))
                {
                    ModelState.AddModelError("OrderID", "Order already exist");
                    return StatusCode(409, "Order already exist");
                }
                else
                {
                    var query = await _orderService.CreateOrder(entity);
                    return StatusCode(200, query);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.StackTrace);
            }
        }

        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(OrderrDTO o)
        {
            try
            {
                if (!await _orderService.ExistsOrder(o.OrderID))
                {
                    return StatusCode(404, "Employee not found");
                }
                else
                {
                    var emp = await _orderService.UpdateOrder(o);

                    return StatusCode(200, emp);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.StackTrace);
            }
        }

        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(OrderrDTO o)
        {
            try
            {
                if (!await _orderService.ExistsOrder(o.OrderID))
                {
                    return StatusCode(404, "Employee Not Found");
                }
                else
                {
                    var flag = await _orderService.DeleteOrder(o);

                    return StatusCode(200, flag);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.StackTrace);
            }
        }
    }
}
