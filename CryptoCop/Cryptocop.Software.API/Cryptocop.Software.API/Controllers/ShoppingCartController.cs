using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Cryptocop.Software.API.Controllers
{
    [Authorize]
    [Route("api/cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        public IActionResult GetAllItemsInShoppingCart()
        {
            var items = _shoppingCartService.GetCartItems(User?.Identity?.Name);
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToShoppingCart([FromBody]ShoppingCartItemInputModel item)
        {
            await _shoppingCartService.AddCartItem(User.Identity?.Name, item);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult RemoveItemFromShoppingCart(int id)
        {
            _shoppingCartService.RemoveCartItem(User.Identity?.Name, id);
            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}")]
        public IActionResult UpdateItemQuantity([FromBody] ShoppingCartItemInputModel item, int id)
        {
            _shoppingCartService.UpdateCartItemQuantity(User.Identity?.Name, id, item.Quantity);
            return NoContent();
        }

        [HttpDelete]
        public IActionResult ClearShoppingCart()
        {
            _shoppingCartService.ClearCart(User.Identity?.Name);
            return NoContent();
        }


    }
}