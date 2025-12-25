using Lab8.Models;

namespace Lab8.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetAll();
        Order? GetById(int id);
        Order? GetByIdWithDetails(int id);
        void Add(Order order);
        void Update(Order order);
        void Delete(int id);
    }
}
