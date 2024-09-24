using MassTransit;
using Producer.Models;
using Shared.Models;

namespace Producer.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _db;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderService(
            AppDbContext db, 
            IPublishEndpoint publishEndpoint)
        {
            _db = db;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Order> SaveOrder(Order model)
        {
            try
            {
                // Save order to database
                _db.Orders.Add(model);
                await _db.SaveChangesAsync();
                // Send order to RabbitMQ
                await _publishEndpoint.Publish<OrderCreated>(model);
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
