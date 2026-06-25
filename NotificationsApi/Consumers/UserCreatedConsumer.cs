using NotificationsApi.Contracts;
using NotificationsApi.Messagens;

namespace NotificationsApi.Consumers
{
    public class UserCreatedConsumer : BackgroundService
    {
        private readonly IRabbitMqConsumerService _messageBus;


        public UserCreatedConsumer(IRabbitMqConsumerService messageBus)
        {
            _messageBus = messageBus;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _messageBus.SubscribeAsync<UserCreatedEvent>("user-created", ConsumeAsync);
        }


        public async Task ConsumeAsync(UserCreatedEvent message)
        {
            Console.WriteLine($"[EMAIL] Bem-vindo {message.Email}");

            await Task.CompletedTask;
        }
    }
}
