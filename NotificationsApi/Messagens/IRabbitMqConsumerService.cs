using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationsApi.Messagens
{
    public interface IRabbitMqConsumerService
    {
        Task PublishAsync<T>(string queue, T message);

        Task SubscribeAsync<T>(
            string queue,
            Func<T, Task> handler);
    }
}
