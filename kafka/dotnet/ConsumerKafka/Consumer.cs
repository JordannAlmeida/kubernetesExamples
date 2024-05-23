using Confluent.Kafka;
using Microsoft.Extensions.Hosting;

namespace ConsumerKafka
{
    public class Consumer : IHostedService
    {
        private readonly IConsumer<Null, string> _consumer;

        public Consumer()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "kafka-broker-1:9092,kafka-broker-2:9093",
                GroupId = "consumer-topic-test",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Null, string>(config).Build();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Subscribe("topic-test");

            var consumeCancellationToken = new CancellationTokenSource();

            Task.Run(() =>
            {
                while (!consumeCancellationToken.IsCancellationRequested)
                {
                    var consumeResult = _consumer.Consume(cancellationToken);
                    Console.WriteLine($"Consumed message: {consumeResult.Message.Value}");
                }
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Close();
            return Task.CompletedTask;
        }
    }
}
