namespace BlazorApp.Models
{
    // Oblik greške koji vraća Web API (BusinessExceptionHandler, Vežbe 6)
    public class ErrorResponse
    {
        public int Status { get; set; }
        public string? Message { get; set; }
    }
}
