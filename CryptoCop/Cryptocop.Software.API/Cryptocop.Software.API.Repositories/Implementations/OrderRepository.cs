using System;
using System.Collections.Generic;
using System.Linq;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.Entities;
using Cryptocop.Software.API.Models.Exceptions;
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
            return _dbContext
                .Orders
                .Where(o => o.Email == email)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    Email = email,
                    FullName = o.FullName,
                    StreetName = o.StreetName,
                    HouseNumber = o.HouseNumber,
                    ZipCode = o.ZipCode,
                    Country = o.Country,
                    City = o.City,
                    CardholderName = o.CardHolderName,
                    CreditCard = o.MaskedCreditCard,
                    OrderDate = o.OrderDate.ToString(),
                    TotalPrice = o.TotalPrice,
                    OrderItems = _dbContext.OrderItems.Where(i => i.OrderId == o.Id).Select(i => new OrderItemDto
                    {
                        Id = i.Id,
                        ProductIdentifier = i.ProductIdentifier,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                        TotalPrice = i.TotalPrice,
                    }).ToList()
                });
        }

        public OrderDto CreateNewOrder(string email, OrderInputModel order)
        {

            var userInfo = _dbContext
                .Users
                .Where(u => u.Email == email)
                .Include(u => u.Addresses.Where(a => a.Id == order.AddressId))
                .Include(u => u.PaymentCards.Where(p => p.Id == order.PaymentCardId))
                .FirstOrDefault();

            if (userInfo.PaymentCards.Count() <= 0) { throw new ResourceDoesNotBelongException($"No credit card with id {order.PaymentCardId} belongs to you."); }
            if (userInfo.Addresses.Count() <= 0) { throw new ResourceDoesNotBelongException($"No address with id {order.AddressId} belongs to you."); }

            var shoppingCartItems = _dbContext
                .ShoppingCartItems
                .Where(i => i.ShoppingCartId == _dbContext
                    .ShoppingCarts
                    .Where(s => s.UserId == userInfo.Id)
                    .Select(s => s.Id)
                    .FirstOrDefault())
                .ToList();

            if(shoppingCartItems.Count() <= 0) { throw new ArgumentOutOfRangeException("You have no items in your shopping cart. Please add some and try again."); }

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
                TotalPrice = shoppingCartItems.Sum(s => s.UnitPrice * s.Quantity)
            };

            _dbContext.Add(orderEntity);
            _dbContext.SaveChanges();

            var orderItems = shoppingCartItems
                .Select(i => new OrderItem
                {
                    OrderId = orderEntity.Id,
                    ProductIdentifier = i.ProductIdentifier,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.Quantity * i.UnitPrice,
                }).ToList();

            _dbContext.AddRange(orderItems);
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
                CreditCard = userInfo.PaymentCards.Select(p => p.CardNumber).FirstOrDefault(),
                OrderDate = orderEntity.OrderDate.ToString(),
                TotalPrice = orderEntity.TotalPrice,
                OrderItems = orderItems.Select(i => new OrderItemDto
                {
                    Id = i.Id,
                    ProductIdentifier = i.ProductIdentifier,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.TotalPrice,
                }).ToList()
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