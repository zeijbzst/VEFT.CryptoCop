using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cryptocop.Software.API.Controllers
{
    [Authorize]
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var orders = _orderService.GetOrders(User.Identity?.Name);
            return Ok(orders);
        }

        [HttpPost]
        public IActionResult CreateNewOrder([FromBody] OrderInputModel order)
        {
            _orderService.CreateNewOrder(User.Identity?.Name, order);
            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}