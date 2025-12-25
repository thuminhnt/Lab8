using Lab8.Models;
using Lab8.Repositories.Interfaces;
using Lab8.Services.Interfaces;

namespace Lab8.Services.Implementtations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }

        public Order? GetOrderById(int id)
        {
            return _orderRepository.GetById(id);
        }

        public Order? GetOrderWithDetails(int id)
        {
            return _orderRepository.GetByIdWithDetails(id);
        }

        public void CreateOrder(Order order)
        {
            _orderRepository.Add(order);
        }

        public void UpdateOrder(Order order)
        {
            _orderRepository.Update(order);
        }

        public void DeleteOrder(int id)
        {
            _orderRepository.Delete(id);
        }
    }
}
