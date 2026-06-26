using NotificationsApi.Contracts;
using NotificationsApi.Messagens;

namespace NotificationsApi.Consumers
{
    public class ProcessarPagamentoConsumer : BackgroundService
    {

        private readonly IRabbitMqConsumerService _messageBus;


        public ProcessarPagamentoConsumer(IRabbitMqConsumerService messageBus)
        {
            _messageBus = messageBus;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageBus.SubscribeAsync<PaymentProcessedEvent>("payment-processed", ConsumeAsync);

            return Task.Delay(Timeout.Infinite, stoppingToken);
        }

        public async Task ConsumeAsync(PaymentProcessedEvent message)
        {
          
            if(message.Status == "Aprovado")
            {
                Console.WriteLine($"[EMAIL] Compra aprovada com sucesso!");
            }
            else
            {
                Console.WriteLine($"[EMAIL] Compra não aprovada!");

            }
        }
    }
}
