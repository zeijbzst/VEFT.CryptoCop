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
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly CryptoDbContext _dbContext;

        public ShoppingCartRepository(CryptoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ShoppingCartItemDto> GetCartItems(string email)
        {
            return _dbContext
                .ShoppingCartItems
                .Where(i => i.ShoppingCart.User.Email == email)
                .Select(i => new ShoppingCartItemDto
                {
                    Id = i.Id,
                    ProductIdentifier = i.ProductIdentifier,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.Quantity * i.UnitPrice
                }).ToList();
        }

        public void AddCartItem(string email, ShoppingCartItemInputModel shoppingCartItem, float priceInUsd)
        {
            var shoppingCart = _dbContext
                .ShoppingCarts
                .Where(s => s.User.Email == email)
                .FirstOrDefault();

            var cartItemEntity = new ShoppingCartItem
            {
                ShoppingCartId = shoppingCart.Id,
                ProductIdentifier = shoppingCartItem.ProductIdentifier,
                Quantity = shoppingCartItem.Quantity,
                UnitPrice = priceInUsd
            };

            _dbContext.Add(cartItemEntity);
            _dbContext.SaveChanges();

        }

        public void RemoveCartItem(string email, int id)
        {
            var item = _dbContext
                .ShoppingCartItems
                .Where(i => i.ShoppingCart.User.Email == email && i.Id == id)
                .FirstOrDefault();
            if (item == null) { return; }
            _dbContext.Remove(item);
            _dbContext.SaveChanges();
        }

        public void UpdateCartItemQuantity(string email, int id, float quantity)
        {
            var item = _dbContext
                .ShoppingCartItems
                .Where(i => i.ShoppingCart.User.Email == email && i.Id == id)
                .FirstOrDefault();
            if (item == null) { throw new ResourceNotFoundException($"Cart item with id {id} does not belong to you."); }

            item.Quantity = (double)quantity;
            _dbContext.SaveChanges();
        }

        public void ClearCart(string email)
        {
            var items = _dbContext
                .ShoppingCartItems
                .Where(i => i.ShoppingCart.User.Email == email);
            _dbContext.RemoveRange(items);
            _dbContext.SaveChanges();
        }

        public void DeleteCart(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}