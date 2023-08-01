using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.IService
{
    public interface ISupplierService
    {
        Task<List<SupplierDTO>> GetSupplierByCity(string city);
        Task<SupplierDTO> CreateSupplier(SupplierDTO supplierDTO);
        Task<SupplierDTO> GetSupplier(int supplierID);
        Task<SupplierDTO> UpdateSupplier(SupplierDTO supplierDTO);
        Task<bool> DeleteSupplier(SupplierDTO supplierDTO);
        Task<bool> ExistsSupplier(int id);
    }
}
