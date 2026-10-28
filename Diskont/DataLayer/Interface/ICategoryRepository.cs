using Entities;

namespace DataLayer.Interface
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category? GetByName(string name);
    }
}
