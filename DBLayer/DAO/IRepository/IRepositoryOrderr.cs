using DBLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.DAO.IRepository
{
    public interface IRepositoryOrderr : IRepository<Orderr>
    {
        Task<List<Orderr>> GetOrdersByCustomerID(string customerId);
        Task<List<Orderr>> GetOrdersByCity(string city);
        Task<bool> Exists(int id);
    }
}
