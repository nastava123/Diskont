namespace WebApi.Dto
{
    // Podaci koje klijent šalje pri registraciji
    public class RegisterUserRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
