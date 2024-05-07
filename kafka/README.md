## Oficial documentation

https://kafka.apache.org/documentation/

## Playlist in portuguese

https://www.youtube.com/watch?v=o5yviW6QSrE&list=PL5aY_NrL1rjt_AZxj11kQjiTNLGg4ZaZA&ab_channel=FullCycle

https://whimsical.com/kafka-EbWjeGL3gDg9apxewMyGhB



### My annotations

- Zookeeper is deprecated. Now we need to use KRaft
- In secutiry case, kafka can encrypt message in transit but it won't store data encrypted into brokers. In this case you can pass a secret in metatada message and the message encrypted. The consumer can use the same algorithm to decrypt message with the secret.
- A lot of example with kafka and docker compose: https://github.com/confluentinc

### how dimension a vm to run a kafka broker:
The recommended specifications for a virtual machine (VM) to run as a Kafka broker depend on several factors, including the expected throughput, number of topics, number of partitions, and the hardware resources available in your environment. Here are some general guidelines for sizing a Kafka broker VM:

**CPU**:
- Kafka is CPU-intensive, especially for operations involving disk I/O. A good starting point is 4-8 vCPUs per broker instance.
- For higher throughput or larger cluster sizes, you may need more vCPUs (8-16 or more).

**Memory (RAM)**:
- Kafka relies heavily on memory for caching and buffering messages. A minimum of 8GB RAM is recommended, but more is better.
- For larger workloads or clusters, allocate 16GB or more RAM per broker instance.
- The memory requirements scale with the number of topics, partitions, and the size of the messages.

**Disk Space**:
- Kafka stores messages on disk, so sufficient disk space is crucial.
- The disk space required depends on the retention period, throughput, and message sizes.
- As a general rule, plan for at least 5-10GB of disk space per partition, depending on your use case.
- Use fast and reliable storage (e.g., SSDs) for better performance.

**Network**:
- Kafka relies heavily on network communication between brokers, producers, and consumers.
- Ensure that the VM has a high-speed network connection with sufficient bandwidth to handle the expected throughput.

**Other Considerations**:
- Use separate disk volumes or partitions for Kafka logs and operating system files.
- Consider using multiple network interfaces for better network isolation and performance.
- If running Kafka in a virtualized environment, ensure that the host has sufficient resources (CPU, RAM, network, and storage) to support the Kafka cluster.

It's essential to monitor and adjust the VM resources based on your actual workload and performance requirements. Start with conservative estimates and scale up or down as needed. Additionally, it's recommended to follow Kafka's best practices for configuration and performance tuning. 