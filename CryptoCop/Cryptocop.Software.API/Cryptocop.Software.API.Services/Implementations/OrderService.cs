using System.Collections.Generic;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IQueueService _queueService;
        private readonly IShoppingCartRepository _cartRepository;

        public OrderService(IOrderRepository orderRepository, IQueueService queueService, IShoppingCartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _queueService = queueService;
            _cartRepository = cartRepository;
        }

        public IEnumerable<OrderDto> GetOrders(string email)
        {
            return _orderRepository.GetOrders(email);
        }

        public void CreateNewOrder(string email, OrderInputModel order)
        {
            var orderDto = _orderRepository.CreateNewOrder(email, order);
            _cartRepository.ClearCart(email);
            _queueService.PublishMessage("create-order", orderDto);
            
        }
    }
}