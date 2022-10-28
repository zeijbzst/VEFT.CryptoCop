using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Services.Helpers;
using Cryptocop.Software.API.Services.Interfaces;


namespace Cryptocop.Software.API.Services.Implementations
{
    public class CryptoCurrencyService : ICryptoCurrencyService
    {
        private HttpClient _httpClient;

        public CryptoCurrencyService(HttpClient httpClien)
        {
            _httpClient = httpClien;
        }

        public async Task<IEnumerable<CryptoCurrencyDto>> GetAvailableCryptocurrencies()
        {
            var response = await _httpClient.GetAsync("v2/assets?fields=id,name,slug,symbol,metrics/market_data/price_usd,profile/general/overview/project_details&limit=500");
            response.EnsureSuccessStatusCode();
            var crypto = await HttpResponseMessageExtensions.DeserializeJsonToList<CryptoCurrencyDto>(response, true);
            return crypto.Where(c => c.Symbol == "BTC" || c.Symbol == "ETH" || c.Symbol == "USDT" || c.Symbol == "XMR");
        }
    }
}