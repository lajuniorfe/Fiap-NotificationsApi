using NotificationsApi.Consumers;
using NotificationsApi.Contracts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace NotificationsApi.BackgroundServices
{
    public class RabbitMqConsumerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private IConnection? _connection;
        private IChannel? _channel;
     

        public RabbitMqConsumerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _connection = await factory.CreateConnectionAsync();
                    break;
                }
                catch
                {
                    Console.WriteLine("RabbitMQ ainda não está pronto... tentando novamente");
                    await Task.Delay(3000, stoppingToken);
                }
            }

            _channel = await _connection!.CreateChannelAsync();

            await StartUserCreatedConsumer();
            await StartPaymentProcessedConsumer();

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }


        private async Task StartUserCreatedConsumer()
        {

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (_, ea) =>
            {
                using var scope = _serviceProvider.CreateScope();

                var userConsumer =
                    scope.ServiceProvider.GetRequiredService<IUserCreatedConsumer>();

                var json = Encoding.UTF8.GetString(ea.Body.ToArray());

                var message =
                    JsonSerializer.Deserialize<UserCreatedEvent>(json);

                if (message != null)
                {
                    await userConsumer.ConsumeAsync(message);
                }

                await _channel.BasicAckAsync(ea.DeliveryTag, false);
            };

            await _channel.BasicConsumeAsync(
                 queue: "user-created",
                 autoAck: false,
                 consumer: consumer);
        }

        private async Task StartPaymentProcessedConsumer()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (_, ea) =>
            {
                using var scope = _serviceProvider.CreateScope();

                var paymentConsumer =
                    scope.ServiceProvider.GetRequiredService<IPaymentProcessedConsumer>();

                var json = Encoding.UTF8.GetString(ea.Body.ToArray());

                var message =
                    JsonSerializer.Deserialize<PaymentProcessedEvent>(json);

                if (message != null)
                {
                    await paymentConsumer.ConsumeAsync(message);
                }

                await _channel.BasicAckAsync(ea.DeliveryTag, false);
            };

            await _channel.BasicConsumeAsync(
               queue: "payment-processed",
               autoAck: false,
               consumer: consumer);
        }
    }
}
