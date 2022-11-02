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

        public OrderService(IOrderRepository orderRepository, IQueueService queueService)
        {
            _orderRepository = orderRepository;
            _queueService = queueService;
        }

        public IEnumerable<OrderDto> GetOrders(string email)
        {
            throw new System.NotImplementedException();
        }

        public void CreateNewOrder(string email, OrderInputModel order)
        {
            var orderDto = _orderRepository.CreateNewOrder(email, order);
            _queueService.PublishMessage("create-order", orderDto);
        }
    }
}