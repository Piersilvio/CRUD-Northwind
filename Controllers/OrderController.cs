﻿using DBLayer.DAO.IRepository;
using DBLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind;
using ServiceLayer.AutoMapper;
using ServiceLayer.DTO;
using ServiceLayer.IService;
using ServiceLayer.Service.ServiceImp;
using System.Net;

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
                    return StatusCode((int)HttpStatusCode.NotFound, "order not found");
                }
                else
                {
                    var orderById = await _orderService.GetOrder(id);
                    return StatusCode((int)HttpStatusCode.OK, orderById);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.StackTrace);
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
                    return StatusCode((int)HttpStatusCode.NotFound, "Orders not found");
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, e);
                }

            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.StackTrace);
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
                    return StatusCode((int)HttpStatusCode.NotFound, "Orders not found");
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, e);
                }
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.StackTrace);
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
                    return StatusCode((int)HttpStatusCode.Conflict, "Order already exist");
                }
                else
                {
                    var query = await _orderService.CreateOrder(entity);
                    return StatusCode((int)HttpStatusCode.OK, query);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.StackTrace);
            }
        }

        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(OrderrDTO o)
        {
            try
            {
                if (!await _orderService.ExistsOrder(o.OrderID))
                {
                    return StatusCode((int)HttpStatusCode.NotFound, "Employee not found");
                }
                else
                {
                    var emp = await _orderService.UpdateOrder(o);

                    return StatusCode((int)HttpStatusCode.OK, emp);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.StackTrace);
            }
        }

        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(OrderrDTO o)
        {
            try
            {
                if (!await _orderService.ExistsOrder(o.OrderID))
                {
                    return StatusCode((int)HttpStatusCode.NotFound, "Employee Not Found");
                }
                else
                {
                    var flag = await _orderService.DeleteOrder(o);

                    return StatusCode((int)HttpStatusCode.OK, flag);
                }
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.StackTrace);
            }
        }
    }
}
