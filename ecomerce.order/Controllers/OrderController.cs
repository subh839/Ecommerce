using Confluent.Kafka;
using System.Text.Json;
using ecommerce.model;
using ecommerce.order.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Confluent.Kafka.ConfigPropertyNames;
using ecommerce.order.Kafka;

namespace ecommerce.order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(OrderDbContext context, IKafkaProducer producer) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<OrderModel>> PostOrder(OrderModel order)
        {
            order.OrderDate = DateTime.Now;
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            var orderMessage = new OrderModel { OrderDate = order.OrderDate };
           

            await producer.ProduceAsync("order-topic", new Message<string, string>
            {
                Key = order.Id.ToString(),
                Value = JsonSerializer.Serialize(orderMessage)
            });

            return order;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderModel>>> GetOrder()
        {
            return await context.Orders.ToListAsync();
        }
    }
}
