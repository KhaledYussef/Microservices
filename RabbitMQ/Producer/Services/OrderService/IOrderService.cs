using Producer.Models;

namespace Producer.Services
{
    public interface IOrderService
    {
        Task<Order> SaveOrder(Order model);
    }
}