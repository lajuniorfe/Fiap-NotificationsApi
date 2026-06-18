using NotificationsApi.Contracts;

namespace NotificationsApi.Consumers
{
    public interface IPaymentProcessedConsumer
    {
        Task ConsumeAsync(PaymentProcessedEvent message);
    }
}