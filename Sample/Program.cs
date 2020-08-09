using System;
using System.Threading.Tasks;
using Tolltech.MassTransitClient;

namespace Tolltech.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bus = new MassTransitRabbitMqBus("rabbitmq://localhost");

            //todo: можно контейнером достать
            var concatStringConsumer = new ConcatStringConsumer();

            bus.AddConsumer(concatStringConsumer);

            await bus.Start();

            string str = null;
            while (str != "exit")
            {
                str = Console.ReadLine();

                await bus.Publish(new ConcatStringEvent {Value = str});
            }

            await bus.Stop();
        }
    }
}