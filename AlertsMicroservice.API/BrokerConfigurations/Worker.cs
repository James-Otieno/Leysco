using System.Reflection;
using EasyNetQ.AutoSubscribe;

namespace AlertsMicroservice.API.BrokerConfigurations
{
    public class Worker : BackgroundService
    {
        private readonly AutoSubscriber _autoSubscriber;

        public Worker(AutoSubscriber autoSubscriber)
        {
            _autoSubscriber = autoSubscriber;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _autoSubscriber
               .SubscribeAsync(new Assembly[] { Assembly
                .GetExecutingAssembly() }, stoppingToken);

        }
    }
}
