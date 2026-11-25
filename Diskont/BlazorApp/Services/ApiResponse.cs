using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorApp.Services
{
    public static class ApiResponse
    {
        // Neuspešan odgovor (4xx, 5xx): čita { status, message } i baca ApiException.
        public static async Task EnsureSuccessAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return;

            string message = $"Greška {(int)response.StatusCode}";
            try
            {
                ErrorResponse? error =
                    await response.Content.ReadFromJsonAsync<ErrorResponse>();
                if (!string.IsNullOrWhiteSpace(error?.Message))
                    message = error.Message;
            }
            catch (JsonException)
            {
                // telo odgovora nije JSON; ostaje poruka sa statusnim kodom
            }

            throw new ApiException((int)response.StatusCode, message);
        }
    }

    // Oblik greške koji vraća BusinessExceptionHandler (Vežbe 6)
    public class ErrorResponse
    {
        public int Status { get; set; }
        public string? Message { get; set; }
    }
}
