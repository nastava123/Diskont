using Entities;

namespace DataLayer.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        User? GetByEmail(string email);
    }
}
