using EasyNetQ.AutoSubscribe;

namespace Alerts.Microservice.Api.BrokerConfigurations
{
    public class MessageDispatcher : IAutoSubscriberMessageDispatcher
    {
        private readonly IServiceProvider _provider;
        public MessageDispatcher(IServiceProvider provider)
        {
            _provider = provider;
        }

        public void Dispatch<TMessage, TConsumer>(TMessage message, CancellationToken cancellationToken)
          where TMessage : class
          where TConsumer : class, IConsume<TMessage>
        {
            using (var scope = _provider.CreateScope())
            {
                var consumer = scope.ServiceProvider.GetRequiredService<TConsumer>();
                consumer.Consume(message, cancellationToken);
            }
        }

        public async Task DispatchAsync<TMessage, TConsumer>(TMessage message, CancellationToken cancellationToken)
           where TMessage : class
           where TConsumer : class, IConsumeAsync<TMessage>
        {
            using (var scope = _provider.CreateScope())
            {
                var consumer = scope.ServiceProvider.GetRequiredService<TConsumer>();
                await consumer.ConsumeAsync(message, cancellationToken);
            }
        }
    }
}
