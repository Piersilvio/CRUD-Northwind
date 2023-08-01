using DBLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.DAO.IRepository
{
    public interface IRepositoryProduct : IRepository<Product>
    {
        Task<List<Product>> GetProductByName(string name);
        Task<bool> Exists(int id);
    }
}
