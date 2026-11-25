using System.Net;
using System.Net.Http.Json;
using Entities;

namespace BlazorApp.Services
{
    // Sve što aplikacija zna o adresi /api/categories nalazi se u ovoj klasi.
    public class CategoryApi
    {
        private readonly HttpClient _http;

        public CategoryApi(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            HttpResponseMessage response = await _http.GetAsync("api/categories");
            await ApiResponse.EnsureSuccessAsync(response);
            return await response.Content.ReadFromJsonAsync<List<Category>>()
                   ?? new List<Category>();
        }

        public async Task<Category?> GetAsync(int id)
        {
            HttpResponseMessage response = await _http.GetAsync($"api/categories/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            await ApiResponse.EnsureSuccessAsync(response);
            return await response.Content.ReadFromJsonAsync<Category>();
        }

        public async Task AddAsync(Category category)
        {
            HttpResponseMessage response =
                await _http.PostAsJsonAsync("api/categories", category);
            await ApiResponse.EnsureSuccessAsync(response);
        }

        public async Task UpdateAsync(Category category)
        {
            HttpResponseMessage response =
                await _http.PutAsJsonAsync($"api/categories/{category.IdCategory}",
                                           category);
            await ApiResponse.EnsureSuccessAsync(response);
        }

        public async Task DeleteAsync(int id)
        {
            HttpResponseMessage response =
                await _http.DeleteAsync($"api/categories/{id}");
            await ApiResponse.EnsureSuccessAsync(response);
        }
    }
}
