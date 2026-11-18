using Microsoft.AspNetCore.Diagnostics;

namespace WebApi.Errors
{
    // Jedno mesto koje izuzetke iz servisa pretvara u HTTP odgovor.
    public class BusinessExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<BusinessExceptionHandler> _logger;

        public BusinessExceptionHandler(ILogger<BusinessExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
                                                    Exception exception,
                                                    CancellationToken cancellationToken)
        {
            int status = exception switch
            {
                ArgumentException => StatusCodes.Status400BadRequest,       // loš unos
                InvalidOperationException => StatusCodes.Status409Conflict, // pravilo
                _ => StatusCodes.Status500InternalServerError               // neočekivano
            };

            string message;
            if (status == StatusCodes.Status500InternalServerError)
            {
                // detalji ostaju u logu servera, klijent ih ne vidi
                _logger.LogError(exception, "Neočekivana greška");
                message = "Došlo je do greške na serveru.";
            }
            else
            {
                message = exception.Message;
            }

            httpContext.Response.StatusCode = status;
            await httpContext.Response.WriteAsJsonAsync(new { status, message },
                                                         cancellationToken);
            return true;    // greška je obrađena
        }
    }
}
