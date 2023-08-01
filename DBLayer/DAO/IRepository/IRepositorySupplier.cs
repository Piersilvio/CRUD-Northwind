using DBLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLayer.DAO.IRepository
{
    public interface IRepositorySupplier : IRepository<Supplier>
    {
        Task<List<Supplier>> GetSupplierByCity(string city);
        Task<bool> Exists(int  id);
    }
}
