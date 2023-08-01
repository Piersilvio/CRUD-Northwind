using AutoMapper;
using ServiceLayer.DTO;
using DBLayer.Models;

namespace ServiceLayer.AutoMapper
{
    public class MapperConfig : IMapperConfig
    {
        public Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //Mpping Employees EmployeesDTO
                cfg.CreateMap<Employee, EmployeeDTO>().ReverseMap();

                //Mapping Customers CustomerDTO
                cfg.CreateMap<Customer, CustomerDTO>();
                cfg.CreateMap<CustomerDTO, Customer>();

                //Mapping Orders OrdersDTO
                cfg.CreateMap<Orderr, OrderrDTO>();
                cfg.CreateMap<OrderrDTO, Orderr>();

                //Mapping Products ProductsDTO
                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<ProductDTO, Product>();

                cfg.CreateMap<Shipper, ShipperDTO>();
                cfg.CreateMap<ShipperDTO, Shipper>();

                //Mapping Suplier SupliersDTO
                cfg.CreateMap<Supplier, SupplierDTO>();
                cfg.CreateMap<SupplierDTO, Supplier>();
            });

            var mapper = new Mapper(config);

            return mapper;
        }
    }
}
