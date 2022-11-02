using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cryptocop.Software.API.Controllers
{
    [Authorize]
    [Route("api/addresses")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public IActionResult GetAddresses()
        {
            var addresses = _addressService.GetAllAddresses(User.Identity?.Name);
            return Ok(addresses);
        }

        [HttpPost]
        public IActionResult AddNewAddress(AddressInputModel address)
        {
            _addressService.AddAddress(User.Identity?.Name, address);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteAddress(int id)
        {
            _addressService.DeleteAddress(User.Identity?.Name, id);
            return NoContent();
        }
    }
}