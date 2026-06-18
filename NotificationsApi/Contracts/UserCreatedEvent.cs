using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationsApi.Contracts
{
    public record UserCreatedEvent(
    Guid UserId,
    string Name,
    string Email
);
}
