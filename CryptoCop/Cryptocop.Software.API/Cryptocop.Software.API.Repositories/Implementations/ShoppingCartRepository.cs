using System.Collections.Generic;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.Exceptions;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Data;
using Cryptocop.Software.API.Repositories.Interfaces;

namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly CryptoDbContext _dbContext;

        public ShoppingCartRepository(CryptoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ShoppingCartItemDto> GetCartItems(string email)
        {
            throw new System.NotImplementedException();
        }

        public void AddCartItem(string email, ShoppingCartItemInputModel shoppingCartItemItem, float priceInUsd)
        {
            var user = _dbContext.Users.Where(u => u.Email == email).FirstOrDefault();
            if (user == null) { throw new UserNotFoundException($"User with email {email} was not found."); }
            

        }

        public void RemoveCartItem(string email, int id)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCartItemQuantity(string email, int id, float quantity)
        {
            throw new System.NotImplementedException();
        }

        public void ClearCart(string email)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCart(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}