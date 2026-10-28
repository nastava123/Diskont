using Entities;

namespace DataLayer.Interface
{
    public interface IOrderRepository : IRepository<Order>
    {
        List<Order> GetAllByUser(int idUser);
        Order? GetOrderByUser(int idUser);      
    }
}
