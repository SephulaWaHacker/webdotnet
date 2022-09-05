using BMW.CloudAdoption.BOM.Core.Helpers;
using BMW.CloudAdoption.BOM.Domain;
using Confluent.Kafka;
using Confluent.Kafka.FactoryExtension.Interfaces.Factories;
using Confluent.Kafka.FactoryExtension.Interfaces.Handlers;

namespace BMW.CloudAdoption.BOM.BackgroundWorkers;

public class BomProducer : BackgroundService
{
    private readonly ILogger<BomProducer> _logger;
    private readonly IProducerFactory _producerFactory;
    private readonly BomQueue _bomQueue;
    private readonly IHostApplicationLifetime _lifetime;

    public BomProducer(
        ILogger<BomProducer> logger, 
        IProducerFactory producerFactory,
        BomQueue bomQueue,
        IHostApplicationLifetime lifetime)
    {
        _logger = logger;
        _producerFactory = producerFactory;
        _bomQueue = bomQueue;
        _lifetime = lifetime;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!await AppStartupHelper.WaitForAppStartup(_lifetime, stoppingToken))
            return;
        
        async void Start() => await BackgroundProcessing(stoppingToken);

        new Thread(Start).Start();
    }

    private async Task BackgroundProcessing(CancellationToken stoppingToken)
    {
        var handle = GetHandle();
        while (!stoppingToken.IsCancellationRequested)
        {
            if (!_bomQueue.TryDequeue(out var bomResponse)) continue;
            
            var message = new Message<string, string>
            {
                Key = bomResponse.VehicleId,
                Value = bomResponse.Serialize()
            };


                var deliveryResult = await handle.ProduceAsync( message, stoppingToken);
                _logger.LogInformation("Bom produced with status: {status} and key: {key}", deliveryResult.Status, deliveryResult.Key);
        }
    }

    private IProducerHandle<string, string> GetHandle()
    {
        var handle = _producerFactory.Create<string, string>(nameof(BomProducer));
            
        handle.Builder.SetErrorHandler((_, error) => _logger.LogError(error.Reason));
        handle.Builder.SetLogHandler((_, message) => _logger.LogInformation(message.Message));

        return handle;
    }
}