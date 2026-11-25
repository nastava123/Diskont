namespace BlazorApp.Services
{
    // Greška koju je vratio Web API: status i poruka iz BusinessExceptionHandler-a
    public class ApiException : Exception
    {
        public int Status { get; }

        public ApiException(int status, string message) : base(message)
        {
            Status = status;
        }
    }
}
