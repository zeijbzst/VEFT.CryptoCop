using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Services.Helpers;
using Cryptocop.Software.API.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cryptocop.Software.API.Services.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        private HttpClient _httpClient;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(HttpClient httpCliet, IShoppingCartRepository shoppingCartRepository)
        {
            _httpClient = httpCliet;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public IEnumerable<ShoppingCartItemDto> GetCartItems(string email)
        {
            throw new System.NotImplementedException();
        }

        public async Task AddCartItem(string email, ShoppingCartItemInputModel shoppingCartItem)
        {
            var response = await _httpClient.GetAsync($"v1/assets/{shoppingCartItem.ProductIdentifer}/metrics/market-data?fields=market_data/price_usd");
            response.EnsureSuccessStatusCode();
            var crypto = await HttpResponseMessageExtensions.DeserializeJsonToObject<CryptoCurrencyDto>(response);
            _shoppingCartRepository.AddCartItem(email, shoppingCartItem, crypto.PriceInUsd);
            return;
        }

        public void RemoveCartItem(string email, int id)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCartItemQuantity(string email, int id, float quantity)
        {
            throw new System.NotImplementedException();
        }

        public void ClearCart(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}
