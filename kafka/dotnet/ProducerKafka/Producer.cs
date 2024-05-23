using Confluent.Kafka;
using Microsoft.Extensions.Hosting;

namespace ProducerKafka
{
    public class Producer : IHostedService
    {
        private readonly IProducer<Null, string> _producer;

        public Producer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "kafka-broker-1:9092,kafka-broker-2:9093"
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 100; i++)
            {
                var message = $"Message {i}";
                _producer.Produce("topic-test", new Message<Null, string> { Value = message }, DeliveryHandler);
            }

            return Task.CompletedTask;
        }

        private void DeliveryHandler(DeliveryReport<Null, string> deliveryReport)
        {
            if (deliveryReport.Error.Code != ErrorCode.NoError)
            {
                Console.WriteLine($"Error producing message: {deliveryReport.Error.Reason}");
            }
            else
            {
                Console.WriteLine($"Produced message to partition {deliveryReport.Partition}, offset {deliveryReport.Offset}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _producer.Flush(cancellationToken);
            return Task.CompletedTask;
        }
    }
}
