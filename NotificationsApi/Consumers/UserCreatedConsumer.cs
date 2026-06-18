using NotificationsApi.Contracts;
using NotificationsApi.Services;

namespace NotificationsApi.Consumers
{
    public class UserCreatedConsumer : IUserCreatedConsumer
    {
        private readonly INotificationService _notificationService;

        public UserCreatedConsumer(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task ConsumeAsync(UserCreatedEvent message)
        {
            Console.WriteLine($"[EMAIL] Bem-vindo {message.Email}");

            await _notificationService.SendWelcomeEmailAsync(message.Email, message.Name);
        }
    }
}
