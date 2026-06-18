using NotificationsApi.Contracts;

namespace NotificationsApi.Consumers
{
    public class PaymentProcessedConsumer : IPaymentProcessedConsumer
    {
        public Task ConsumeAsync(PaymentProcessedEvent message)
        {
            Console.WriteLine($"[EMAIL] Compra confirmada para usuário {message.UserId}");

            return Task.CompletedTask;
        }
    }
}
