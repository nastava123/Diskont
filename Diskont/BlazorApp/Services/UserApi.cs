using BlazorApp.Pages;
using System.Net.Http.Json;

namespace BlazorApp.Services
{
    public class UserApi
    {
        private readonly HttpClient httpClient;

        public UserApi(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public  async  Task<List<User>> GetAllUsers()
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync("/api/users");
            return await responseMessage.Content.ReadFromJsonAsync<List<User>>() ??
                new List<User>();
        }
    }
}
