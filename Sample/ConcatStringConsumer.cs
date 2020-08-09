using System;
using System.Threading.Tasks;
using MassTransit;
using Tolltech.MassTransitClient;

namespace Tolltech.Sample
{
    public class ConcatStringConsumer : IMassTransitConsumer<ConcatStringEvent>
    {
        public Task Consume(ConsumeContext<ConcatStringEvent> context)
        {
            Console.WriteLine($"{context.Message.Value}_{context.Message.Value}");
            return Task.CompletedTask;
        }

        public string QueueName => "ConcatString_Queue";
    }
}