using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.IService
{
    public interface IOrderrService
    {
        Task<List<OrderrDTO>> GetOrdersByCustomerID(string customerID);
        Task<List<OrderrDTO>> GetOrderByCity(string city);
        Task<OrderrDTO> CreateOrder(OrderrDTO orderDTO);
        Task<OrderrDTO> GetOrder(int orderID);
        Task<OrderrDTO> UpdateOrder(OrderrDTO orderDTO);
        Task<bool> DeleteOrder(OrderrDTO orderDTO);
        Task<bool> ExistsOrder(int orderID);
    }
}
