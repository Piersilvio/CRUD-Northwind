using AutoMapper;
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
    public class SupplierService : ISupplierService
    {
        private readonly IRepositorySupplier _supplierRepository;
        private readonly IMapperConfig _mapper;

        public SupplierService(IRepositorySupplier supplierRepository, IMapperConfig mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }
        public async Task<SupplierDTO> CreateSupplier(SupplierDTO supplierDTO)
        {
            var map = _mapper.InitializeAutomapper();

            var supplierToSend = map.Map<Supplier>(supplierDTO);
            var createSup = await _supplierRepository.Create(supplierToSend);
            var supplierToSave = map.Map<SupplierDTO>(createSup);
            return supplierToSave;
        }

        public async Task<bool> DeleteSupplier(SupplierDTO supplierDTO)
        {
            var map = _mapper.InitializeAutomapper();

            var shipperToSend = map.Map<Supplier>(supplierDTO);
            var deleteShip = await _supplierRepository.Delete(shipperToSend);
            return deleteShip;
        }

        public async Task<bool> ExistsSupplier(int id)
        {
            var existSup = await _supplierRepository.Exists(id);
            return existSup;
        }

        public async Task<SupplierDTO> GetSupplier(int id)
        {
            var map = _mapper.InitializeAutomapper();

            var byIdSup = await _supplierRepository.Get(id);
            var supplierToSave = map.Map<SupplierDTO>(byIdSup);
            return supplierToSave;
        }

        public async Task<List<SupplierDTO>> GetSupplierByCity(string city)
        {
            var map = _mapper.InitializeAutomapper();

            var supList = await _supplierRepository.GetSupplierByCity(city);
            var supListToSave = map.Map<List<SupplierDTO>>(supList);

            return supListToSave;
        }

        public async Task<SupplierDTO> UpdateSupplier(SupplierDTO supplierDTO)
        {
            var map = _mapper.InitializeAutomapper();

            var supplierToSend = map.Map<Supplier>(supplierDTO);

            supplierToSend.SupplierID = supplierDTO.SupplierID;
            supplierToSend.CompanyName = supplierDTO.CompanyName;
            supplierToSend.ContactName = supplierDTO.ContactName;
            supplierToSend.ContactTitle = supplierDTO.ContactTitle;
            supplierToSend.Address = supplierDTO.Address;
            supplierToSend.City = supplierDTO.City;
            supplierToSend.Region = supplierDTO.Region;
            supplierToSend.PostalCode = supplierDTO.PostalCode;
            supplierToSend.Country = supplierDTO.Country;
            supplierToSend.Phone = supplierDTO.Phone;
            supplierToSend.Fax = supplierDTO.Fax;
            supplierToSend.HomePage = supplierDTO.HomePage;

            var updateSup = await _supplierRepository.Update(supplierToSend);
            var supplierToSave = map.Map<SupplierDTO>(updateSup);
            return supplierToSave;
        }
    }
}
