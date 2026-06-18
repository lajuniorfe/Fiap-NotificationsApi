using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationsApi.Services
{
    public interface INotificationService
    {
        Task SendWelcomeEmailAsync(string name, string email);

        Task SendPurchaseConfirmationAsync(Guid userId, Guid gameId);
    }
}
