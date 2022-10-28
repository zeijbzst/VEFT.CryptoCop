using Cryptocop.Software.API.Models.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace Cryptocop.Software.API.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllItemsInShoppingCart()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult AddItemToShoppingCart([FromBody]ShoppingCartItemInputModel item)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult RemoveItemFromShoppingCart([FromBody] ShoppingCartItemInputModel item)
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult UpdateItemQuantity([FromBody] ShoppingCartItemInputModel item)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public IActionResult ClearShoppingCart([FromBody] ShoppingCartItemInputModel item)
        {
            throw new NotImplementedException();
        }


    }
}