using System.Collections.Generic;
using System.Linq;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.Entities;
using Cryptocop.Software.API.Models.Exceptions;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Data;
using Cryptocop.Software.API.Repositories.Interfaces;

namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class AddressRepository : IAddressRepository
    {
        private readonly CryptoDbContext _dbContext;

        public AddressRepository(CryptoDbContext context)
        {
            _dbContext = context;
        }

        public void AddAddress(string email, AddressInputModel address)
        {
            var userId = _dbContext
                .Users
                .Where(u => u.Email == email)
                .Select(u => u.Id)
                .FirstOrDefault();

            var addressEntity = new Address
            {
                UserId = userId,
                StreetName = address.StreetName,
                HouseNumber = address.HouseNumber,
                ZipCode = address.ZipCode,
                Country = address.Country,
                City = address.City
            };

            _dbContext.Add(addressEntity);
            _dbContext.SaveChanges();
        }

        public IEnumerable<AddressDto> GetAllAddresses(string email)
        {
            return _dbContext
                .Addresses
                .Where(a => a.User.Email == email)
                .Select(a => new AddressDto
                {
                    Id = a.Id,
                    StreetName = a.StreetName,
                    HouseNumber = a.HouseNumber,
                    ZipCode = a.ZipCode,
                    Country = a.Country,
                    City = a.City
                });
        }

        public void DeleteAddress(string email, int addressId)
        {
            var address = _dbContext
                .Addresses
                .Where(a => a.User.Email == email && a.Id == addressId)
                .FirstOrDefault();
            if (address == null) { return; }

            _dbContext.Remove(address);
            _dbContext.SaveChanges();
        }
    }
}