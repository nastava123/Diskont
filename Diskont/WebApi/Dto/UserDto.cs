using Entities;

namespace WebApi.Dto
{
    // Ono što API vraća o korisniku. Namerno NEMA PasswordHash.
    public class UserDto
    {
        public int IdUser { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int IdRole { get; set; }

        public static UserDto From(User user)
        {
            UserDto dto = new UserDto();
            dto.IdUser = user.IdUser;
            dto.FirstName = user.FirstName;
            dto.LastName = user.LastName;
            dto.Email = user.Email;
            dto.Status = user.Status;
            dto.CreatedDate = user.CreatedDate;
            dto.IdRole = user.IdRole;
            return dto;
        }
    }
}
