using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cryptocop.Software.API.Controllers
{
    [Authorize]
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public IActionResult GetAllCards()
        {
            var cards = _paymentService.GetStoredPaymentCards(User.Identity?.Name);
            return Ok(cards);
        }

        [HttpPost]
        public IActionResult AddPayment(PaymentCardInputModel card)
        {
            _paymentService.AddPaymentCard(User.Identity?.Name, card);
            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}