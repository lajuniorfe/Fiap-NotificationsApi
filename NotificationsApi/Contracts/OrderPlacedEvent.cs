using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationsApi.Contracts
{
    public record OrderPlacedEvent(
      Guid UserId,
      Guid GameId,
      decimal Price
  );
}
