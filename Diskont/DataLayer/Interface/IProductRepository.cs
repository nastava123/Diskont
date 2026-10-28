using Entities;

namespace DataLayer.Interface
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetByCategory(int idCategory);
    }
}
