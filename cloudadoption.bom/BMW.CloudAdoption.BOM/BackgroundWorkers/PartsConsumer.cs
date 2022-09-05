using BMW.CloudAdoption.BOM.Persistence.Cache;
using Confluent.Kafka;
using Confluent.Kafka.FactoryExtension.Interfaces.Factories;
using Confluent.Kafka.FactoryExtension.Interfaces.Handlers;

namespace BMW.CloudAdoption.BOM.BackgroundWorkers;

public class PartsConsumer : BackgroundService
{
    private readonly IConsumerFactory _consumerFactory;
    private readonly ILogger<PartsConsumer> _logger;
    private readonly IPartsCache _partsCache;
    private readonly IHostApplicationLifetime _lifetime;

    public PartsConsumer(IConsumerFactory consumerFactory,
        ILogger<PartsConsumer> logger,
        IPartsCache partsCache,
        IHostApplicationLifetime lifetime)
    {
        _consumerFactory = consumerFactory;
        _logger = logger;
        _partsCache = partsCache;
        _lifetime = lifetime;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!await AppStartupHelper.WaitForAppStartup(_lifetime, stoppingToken))
            return;

        async void Start() => await StartConsumerLoopAsync(stoppingToken);

        new Thread(Start).Start();
    }

    private Task StartConsumerLoopAsync(CancellationToken cancellationToken)
    {
        var handle = GetHandle<string, string>();

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var cr = handle.Consume(cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();

                _logger.LogInformation(
                    "New message consumed from Topic: {Topic} and partition {Partition} and offset {Offset}:: Key={Key} Value={Value}",
                    cr.Topic, cr.Partition, cr.Offset, cr.Message.Key, cr.Message.Value);

                _partsCache.Upsert(cr.Message.Key, cr.Message.Value);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (ConsumeException e)
            {
                if (e.Error.Code != ErrorCode.UnknownTopicOrPart)
                    _logger.LogError(e, e.Error.Reason);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }

        return Task.CompletedTask;
    }

    private IConsumerHandle<TKey, TValue> GetHandle<TKey, TValue>()
    {
        var handle = _consumerFactory.Create<TKey, TValue>(nameof(PartsConsumer));

        handle.Builder
            .SetErrorHandler((_, error) => _logger.LogError(error.Reason))
            .SetLogHandler((_, message) => _logger.LogInformation(message.Message))
            .SetPartitionsAssignedHandler((c, list) =>
            {
                foreach (var partition in list)
                    _logger.LogInformation("Topic - {Topic} = Assigned to PartitionId - {Value}", partition.Topic,
                        partition.Partition.Value);

                var partitionOffsets = c.Committed(list, TimeSpan.FromSeconds(10));
                var watermarkOffsets = list.Select(tp => c.QueryWatermarkOffsets(tp, TimeSpan.FromSeconds(10)));
                var offsets = watermarkOffsets.Zip(partitionOffsets, (watermarkOffset, topicPartitionOffset) =>
                {
                    if (topicPartitionOffset.Offset.IsSpecial || watermarkOffset.High.IsSpecial)
                    {
                        return topicPartitionOffset.Offset;
                    }

                    return new Offset(0);
                });

                return list.Zip(offsets, (partition, offset) => new TopicPartitionOffset(partition, offset));
            })
            .SetPartitionsRevokedHandler((_, list) =>
            {
                foreach (var partition in list)
                    _logger.LogInformation("Topic - {Topic}  = Revoked from PartitionId - {Value}", partition.Topic,
                        partition.Partition.Value);
            })
            .SetPartitionsLostHandler((_, list) =>
            {
                foreach (var partition in list)
                    _logger.LogInformation("Topic - {Topic}  = Lost from PartitionId - {Value}", partition.Topic,
                        partition.Partition.Value);
            });
        return handle;
    }
}