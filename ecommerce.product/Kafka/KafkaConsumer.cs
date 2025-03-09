﻿using Confluent.Kafka;
using ecommerce.model;
using ecommerce.product.Data;
using Newtonsoft.Json;

namespace ecommerce.product.Kafka
{
    public class KafkaConsumer(IServiceScopeFactory scopeFactory) : BackgroundService
    {

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            return Task.Run(() =>
            {
                _ = ConsumeAsync("order-topic", stoppingToken);
            }, stoppingToken);
        }

        public async Task ConsumeAsync(string topic, CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = "order-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using var consumer = new ConsumerBuilder<string, string>(config).Build();

            consumer.Subscribe(topic);
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(stoppingToken);

                var order = JsonConvert.DeserializeObject<OrderModel>(consumeResult.Message.Value);
                using var scope = scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();

                var product = await dbContext.Products.FindAsync(order.ProductId);
                if (product != null)
                {
                    product.Quantity -= order.Quantity;
                    await dbContext.SaveChangesAsync();
                }
            }
            consumer.Close();
        }
    }
}

