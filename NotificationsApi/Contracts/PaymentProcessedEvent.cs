using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationsApi.Contracts
{
    public record PaymentProcessedEvent(
    Guid UserId,
    Guid GameId,
    string Status
);
}
