using System;
using System.Collections.Generic;
using System.Linq;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.Entities;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Data;
using Cryptocop.Software.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CryptoDbContext _dbContext;

        public OrderRepository(CryptoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<OrderDto> GetOrders(string email)
        {
            throw new NotImplementedException();
        }

        public OrderDto CreateNewOrder(string email, OrderInputModel order)
        {

            var userInfo = _dbContext
                .Users
                .Where(u => u.Email == email)
                .Include(u => u.Addresses.Where(a => a.Id == order.AddressId))
                .Include(u => u.PaymentCards.Where(p => p.Id == order.PaymentCardId))
                .FirstOrDefault();

            var shoppingCartItems = _dbContext
                .ShoppingCarts
                .Where(c => c.UserId == userInfo.Id)
                .Include(c => c.ShoppingCartItems
                        .Where(i => i.ShoppingCartId == c.Id))
                .Select(c => c.ShoppingCartItems.Select(i => new ShoppingCartItemDto
                {
                    Id = i.Id,
                    ProductIdentifier = i.ProductIdentifier,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.Quantity * i.UnitPrice
                })).ToList();

            double totalPrice = 0;
            foreach(var shoppingCartItem in shoppingCartItems)
            {
                totalPrice += shoppingCartItem.Select(s => s.UnitPrice).FirstOrDefault() * shoppingCartItem.Select(s => s.Quantity).FirstOrDefault();
            }

            var orderEntity = new Order
            {
                Email = email,
                FullName = userInfo.FullName,
                StreetName = userInfo.Addresses.Select(a => a.StreetName).FirstOrDefault(),
                HouseNumber = userInfo.Addresses.Select(a => a.HouseNumber).FirstOrDefault(),
                ZipCode = userInfo.Addresses.Select(a => a.ZipCode).FirstOrDefault(),
                Country = userInfo.Addresses.Select(a => a.Country).FirstOrDefault(),
                City = userInfo.Addresses.Select(a => a.City).FirstOrDefault(),
                CardHolderName = userInfo.PaymentCards.Select(p => p.CardHolderName).FirstOrDefault(),
                MaskedCreditCard = MaskCredit(userInfo.PaymentCards.Select(p => p.CardNumber).FirstOrDefault()),
                TotalPrice = shoppingCartItems.Sum(s => s.Select(s => s.UnitPrice).FirstOrDefault() * s.Select(s => s.Quantity).FirstOrDefault())
            };


            _dbContext.Add(orderEntity);
            _dbContext.SaveChanges();

            

            return new OrderDto
            {
                Id = orderEntity.Id,
                Email = email,
                FullName = orderEntity.FullName,
                StreetName = orderEntity.StreetName,
                HouseNumber = orderEntity.HouseNumber,
                ZipCode = orderEntity.ZipCode,
                Country = orderEntity.Country,
                City = orderEntity.City,
                CardholderName = orderEntity.CardHolderName,
                CreditCard = _dbContext
                    .Paymentcards
                    .Where(c => c.Id == order.PaymentCardId)
                    .Select(c => c.CardNumber)
                    .FirstOrDefault(),
                OrderDate = orderEntity.OrderDate.ToString(),
                TotalPrice = orderEntity.TotalPrice,
            };
        }

        private string MaskCredit(string creditCard, int tail=4)
        {
            if(tail >= creditCard.Length) return creditCard;
            var lastFour = creditCard[^tail..];
            var mask = new string('*', creditCard.Length - tail);
            return mask + lastFour;
        }
    }
}