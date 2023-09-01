using WebApp.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApp.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var response = await _httpClient.GetAsync("api/products/GetProducts");

            if (response.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
                return products;
            }
            else
            {
                // You should handle errors appropriately, like logging or throwing an exception.
                // Here, I'm throwing an exception for demonstration purposes.
                throw new HttpRequestException($"Failed to fetch products. Status code: {response.StatusCode}");
            }
        }
    }
}
