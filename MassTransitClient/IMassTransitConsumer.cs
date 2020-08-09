using MassTransit;

namespace Tolltech.MassTransitClient
{
    public interface IMassTransitConsumer<in T> : IConsumer<T> where T : class
    {
        string QueueName { get; }
    }
}