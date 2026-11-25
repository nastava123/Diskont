using System.Net;
using System.Net.Http.Json;
using Entities;

namespace BlazorApp.Services
{
    // Klijent za /api/products: iste operacije kao CategoryApi + filter po kategoriji
    public class ProductApi
    {
        private readonly HttpClient _http;

        public ProductApi(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Product>> GetAllAsync(int? idCategory = null)
        {
            string url = "api/products";
            if (idCategory.HasValue)
                url += $"?idCategory={idCategory.Value}";

            HttpResponseMessage response = await _http.GetAsync(url);
            await ApiResponse.EnsureSuccessAsync(response);
            return await response.Content.ReadFromJsonAsync<List<Product>>()
                   ?? new List<Product>();
        }

        public async Task<Product?> GetAsync(int id)
        {
            HttpResponseMessage response = await _http.GetAsync($"api/products/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            await ApiResponse.EnsureSuccessAsync(response);
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        public async Task AddAsync(Product product)
        {
            HttpResponseMessage response =
                await _http.PostAsJsonAsync("api/products", product);
            await ApiResponse.EnsureSuccessAsync(response);
        }

        public async Task UpdateAsync(Product product)
        {
            HttpResponseMessage response =
                await _http.PutAsJsonAsync($"api/products/{product.IdProduct}",
                                           product);
            await ApiResponse.EnsureSuccessAsync(response);
        }

        public async Task DeleteAsync(int id)
        {
            HttpResponseMessage response =
                await _http.DeleteAsync($"api/products/{id}");
            await ApiResponse.EnsureSuccessAsync(response);
        }
    }
}
