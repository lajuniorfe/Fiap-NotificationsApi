using NotificationsApi.BackgroundServices;
using NotificationsApi.Consumers;
using NotificationsApi.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<IUserCreatedConsumer, UserCreatedConsumer>();
builder.Services.AddScoped<IPaymentProcessedConsumer, PaymentProcessedConsumer>();

builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddHostedService<RabbitMqConsumerService>();

var host = builder.Build();
await host.RunAsync();