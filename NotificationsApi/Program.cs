using NotificationsApi.Consumers;
using NotificationsApi.Messagens;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<ProcessarPagamentoConsumer>();
builder.Services.AddHostedService<UserCreatedConsumer>();

builder.Services.AddSingleton<IRabbitMqConsumerService, RabbitServices>();



var host = builder.Build();
await host.RunAsync();