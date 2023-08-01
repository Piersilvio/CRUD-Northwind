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
    public class ProductService : IProductService
    {
        private readonly IRepositoryProduct repositoryProduct;
        private readonly IMapperConfig mapperConfig;

        public ProductService(IRepositoryProduct repositoryProduct, IMapperConfig mapperConfig)
        {
            this.repositoryProduct = repositoryProduct;
            this.mapperConfig = mapperConfig;
        }

        public async Task<ProductDTO> CreateProduct(ProductDTO productDTO)
        {
            var map = mapperConfig.InitializeAutomapper();

            var productToSend = map.Map<Product>(productDTO);
            var createProd = await repositoryProduct.Create(productToSend);
            var productToSave = map.Map<ProductDTO>(createProd);

            return productToSave;
        }

        public async Task<bool> DeleteProduct(ProductDTO productDTO)
        {
            var map = mapperConfig.InitializeAutomapper();

            var productToSend = map.Map<Product>(productDTO);
            var deleteProd = await repositoryProduct.Delete(productToSend);

            return deleteProd;
        }

        public async Task<bool> ExistsProduct(int productID)
        {
            var map = mapperConfig.InitializeAutomapper();

            var existProd = await repositoryProduct.Exists(productID);
            return existProd;
        }

        public async Task<ProductDTO> GetProduct(int productID)
        {
            var map = mapperConfig.InitializeAutomapper();

            var byIdProd = await repositoryProduct.Get(productID);
            var productToSave = map.Map<ProductDTO>(byIdProd);
            return productToSave;
        }

        public async Task<List<ProductDTO>> GetProductByName(string productName)
        {
            var map = mapperConfig.InitializeAutomapper();

            var prList = await repositoryProduct.GetProductByName(productName);
            var prListToSave = map.Map<List<ProductDTO>>(prList);

            return prListToSave;
        }

        public async Task<ProductDTO> UpdateProduct(ProductDTO productDTO)
        {
            var map = mapperConfig.InitializeAutomapper();
            var prToSend = map.Map<Product>(productDTO);

            prToSend.ProductID = productDTO.ProductID;
            prToSend.ProductName = productDTO.ProductName;
            prToSend.SupplierID = productDTO.SupplierID;
            prToSend.CategoryID = productDTO.CategoryID;
            prToSend.QuantityPerUnit = productDTO.QuantityPerUnit;
            prToSend.UnitPrice = productDTO.UnitPrice;
            prToSend.UnitsInStock = productDTO.UnitsInStock;
            prToSend.UnitsOnOrder = productDTO.UnitsOnOrder;
            prToSend.ReorderLevel = productDTO.ReorderLevel;
            prToSend.Discontinued = productDTO.Discontinued;

            var updatePr = await repositoryProduct.Update(prToSend);
            var prToSave = map.Map<ProductDTO>(updatePr);

            return prToSave;
        }
    }
}
