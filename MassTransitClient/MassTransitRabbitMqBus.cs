using System.Threading.Tasks;
using MassTransit;

namespace Tolltech.MassTransitClient
{
    //todo: класс немногопоточный
    public class MassTransitRabbitMqBus
    {
        private readonly string rabbitMqHostName;
        private readonly IBusControl bus;
        private bool started;

        public MassTransitRabbitMqBus(string rabbitMqHostName)
        {
            this.rabbitMqHostName = rabbitMqHostName;
            bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(rabbitMqHostName);
            });
        }


        public void AddConsumer<T>(IMassTransitConsumer<T> consumer) where T : class
        {
            if (started)
            {
                throw new MassTransitBusConfigurationExcpetion($"Can't connect {consumer.GetType().Name}, because bus for {rabbitMqHostName} was started");
            }

            bus.ConnectReceiveEndpoint(consumer.QueueName, configurator => configurator.Instance(consumer));
        }

        public async Task Start()
        {
            if (started)
            {
                return;
            }

            await bus.StartAsync().ConfigureAwait(false);

            started = true;
        }

        public Task Publish<T>(T msg) where T : class
        {
            if (!started)
            {
                throw new MassTransitBusConfigurationExcpetion($"Can't publish {typeof(T).Name}, because bus for {rabbitMqHostName} was not started");
            }

            return bus.Publish(msg);
        }
        
        public Task Stop()
        {
            if (bus == null || !started)
            {
                return Task.CompletedTask;
            }

            return bus.StopAsync();
        }
    }
}