using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationsApi.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
            ILogger<NotificationService> logger)
        {
            _logger = logger;
        }

        public Task SendWelcomeEmailAsync(
            string name,
            string email)
        {
            _logger.LogInformation(
                "[EMAIL] Bem-vindo {Name}! E-mail enviado para {Email}",
                name,
                email);

            return Task.CompletedTask;
        }

        public Task SendPurchaseConfirmationAsync(
            Guid userId,
            Guid gameId)
        {
            _logger.LogInformation(
                "[EMAIL] Compra confirmada. Usuário: {UserId} | Jogo: {GameId}",
                userId,
                gameId);

            return Task.CompletedTask;
        }
    }
}
