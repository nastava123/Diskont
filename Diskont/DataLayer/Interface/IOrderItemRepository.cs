using Entities;

namespace DataLayer.Interface
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        List<OrderItem> GetByOrder(int idOrder);
        OrderItem? GetByOrderAndProduct(int idOrder, int idProduct);
    }
}
