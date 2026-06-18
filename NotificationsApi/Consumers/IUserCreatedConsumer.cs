using NotificationsApi.Contracts;

namespace NotificationsApi.Consumers
{
    public interface IUserCreatedConsumer
    {
        Task ConsumeAsync(UserCreatedEvent message);
    }
}