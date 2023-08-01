using DBLayer.DAO.DAOImpl;
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
    public class OrderService : IOrderrService
    {
        private readonly IRepositoryOrderr repositoryOrder;
        private readonly IMapperConfig mapperConfig;

        public OrderService(IRepositoryOrderr repositoryOrder, IMapperConfig mapperConfig)
        {
            this.repositoryOrder = repositoryOrder;
            this.mapperConfig = mapperConfig;
        }

        public async Task<OrderrDTO> CreateOrder(OrderrDTO orderDTO)
        {
            var map = mapperConfig.InitializeAutomapper();
            var orderToSend = map.Map<Orderr>(orderDTO);
            var createOrd = await repositoryOrder.Create(orderToSend);
            var orderToSave = map.Map<OrderrDTO>(createOrd);

            return orderToSave;
        }

        public async Task<bool> DeleteOrder(OrderrDTO orderDTO)
        {
            var map = mapperConfig.InitializeAutomapper();

            var orderToSend = map.Map<Orderr>(orderDTO);
            var deleteOrder = await repositoryOrder.Delete(orderToSend);

            return deleteOrder;
        }

        public async Task<bool> ExistsOrder(int orderID)
        {
            var map = mapperConfig.InitializeAutomapper();

            var existsOrder = await repositoryOrder.Exists(orderID);
            return existsOrder;
        }

        public async Task<OrderrDTO> GetOrder(int orderID)
        {
            var map = mapperConfig.InitializeAutomapper();

            var byIdOrder = await repositoryOrder.Get(orderID);
            var ordToSave = map.Map<OrderrDTO>(byIdOrder);
            return ordToSave;
        }

        public async Task<List<OrderrDTO>> GetOrderByCity(string city)
        {
            var map = mapperConfig.InitializeAutomapper();

            var orderList = await repositoryOrder.GetOrdersByCity(city);
            var orderListToSave = map.Map<List<OrderrDTO>>(orderList);

            return orderListToSave;
        }

        public async Task<List<OrderrDTO>> GetOrdersByCustomerID(string customerID)
        {
            var map = mapperConfig.InitializeAutomapper();

            var orderList = await repositoryOrder.GetOrdersByCustomerID(customerID);
            var orderListToSave = map.Map<List<OrderrDTO>>(orderList);

            return orderListToSave;
        }

        public async Task<OrderrDTO> UpdateOrder(OrderrDTO orderDTO)
        {
            var map = mapperConfig.InitializeAutomapper();

            var orderToSend = map.Map<Orderr>(orderDTO);

            orderToSend.OrderID = orderDTO.OrderID;
            orderToSend.CustomerID = orderDTO.CustomerID;
            orderToSend.EmployeeID = orderDTO.EmployeeID;
            orderToSend.OrderDate = orderDTO.OrderDate;
            orderToSend.RequiredDate = orderDTO.RequiredDate;
            orderToSend.ShippedDate = orderDTO.ShippedDate;
            orderToSend.ShipVia = orderDTO.ShipVia;
            orderToSend.Freight = orderDTO.Freight;
            orderToSend.ShipName = orderDTO.ShipName;
            orderToSend.ShipCity = orderDTO.ShipCity;
            orderToSend.ShipAddress = orderDTO.ShipAddress;
            orderToSend.ShipRegion = orderDTO.ShipRegion;
            orderToSend.ShipPostalCode = orderDTO.ShipPostalCode;
            orderToSend.ShipCountry = orderDTO.ShipCountry;

            var updateOrder = await repositoryOrder.Update(orderToSend);
            var orderToSave = map.Map<OrderrDTO>(updateOrder);
            return orderToSave;
        }
    }
}
