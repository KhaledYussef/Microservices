using Producer.Models;
using Producer.RabbitMQ;

namespace Producer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMessageProducer _messageProducer;
        private readonly AppDbContext _db;

        public OrderService(IMessageProducer messageProducer, AppDbContext db)
        {
            _messageProducer = messageProducer;
            _db = db;
        }

        public async Task<Order> SaveOrder(Order model)
        {
            try
            {
                // Save order to database
                _db.Orders.Add(model);
                await _db.SaveChangesAsync();
                // Send order to RabbitMQ
                _messageProducer.SendMessage(model);
                // return model
                return model;

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
