using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.IService
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetProductByName(string productName);
        Task<ProductDTO> CreateProduct(ProductDTO productDTO);
        Task<ProductDTO> UpdateProduct(ProductDTO productDTO);
        Task<ProductDTO> GetProduct(int productID);
        Task<bool> DeleteProduct(ProductDTO productDTO);
        Task<bool> ExistsProduct(int productID);
    }
}
