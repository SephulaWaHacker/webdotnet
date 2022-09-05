using BMW.CloudAdoption.BOM.BackgroundWorkers;
using BMW.CloudAdoption.BOM.Core.Extensions;
using BMW.CloudAdoption.BOM.Core.Models;
using BMW.CloudAdoption.BOM.Domain;
using BMW.CloudAdoption.BOM.Persistence.Cache;
using BMW.CloudAdoption.BOM.Persistence.Context;
using Confluent.Kafka.FactoryExtension.Extensions;
using Confluent.Kafka.FactoryExtension.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder);
ConfigureLogging(builder);
ConfigureKafka(builder);
ConfigureDatabase(builder);

var app = builder.Build();
ConfigureApplication(app);

app.RunAsync();

static void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.RegisterModules();
    builder.Services.AddHostedService<PartsConsumer>();
    builder.Services.AddHostedService<BomProducer>();
    builder.Services.AddSingleton<IPartsCache, PartsCache>();
    builder.Services.AddSingleton<BomQueue>();
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.AddMediatR(typeof(Program));
}

static void ConfigureLogging(WebApplicationBuilder builder)
{
    builder.Host.ConfigureLogging(loggingBuilder =>
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddConsole();
    });
}

static void ConfigureKafka(WebApplicationBuilder builder)
{
    var settings = builder.Configuration.Get<Settings>();
    var kafkaSettings = builder.Configuration.GetSection(nameof(KafkaSettings)).Get<KafkaSettings>();

    var partsConsumer = kafkaSettings.Consumers.First(x => x.Key == nameof(PartsConsumer));
    partsConsumer.Value.Config.BootstrapServers = settings.KafkaBootstrapServer;
    partsConsumer.Value.Config.SaslUsername = settings.KafkaUsername;
    partsConsumer.Value.Config.SaslPassword = settings.KafkaPassword;
    partsConsumer.Value.Config.SaslMechanism = settings.KafkaSaslMechanism;
    partsConsumer.Value.Config.SslEndpointIdentificationAlgorithm =
        settings.KafkaSslEndpointIdentificationAlgorithm;
    partsConsumer.Value.Config.SecurityProtocol = settings.KafkaSecurityProtocol;
    partsConsumer.Value.Topic = settings.KafkaPartsTopic;
    partsConsumer.Value.Config.SslCaLocation = settings.KafkaCaLocation;

    var bomProducer = kafkaSettings.Producers.First(x => x.Key == nameof(BomProducer));
    bomProducer.Value.Config.BootstrapServers = settings.KafkaBootstrapServer;
    bomProducer.Value.Config.SaslUsername = settings.KafkaUsername;
    bomProducer.Value.Config.SaslPassword = settings.KafkaPassword;
    bomProducer.Value.Config.SaslMechanism = settings.KafkaSaslMechanism;
    bomProducer.Value.Config.SslEndpointIdentificationAlgorithm = settings.KafkaSslEndpointIdentificationAlgorithm;
    bomProducer.Value.Config.SecurityProtocol = settings.KafkaSecurityProtocol;
    bomProducer.Value.Topic = settings.KafkaBomTopic;
    bomProducer.Value.Config.SslCaLocation = settings.KafkaCaLocation;

    builder.Services.TryAddKafkaFactories(kafkaSettings);
}

static void ConfigureDatabase(WebApplicationBuilder builder)
{
    var settings = builder.Configuration.Get<Settings>();
    builder.Services.AddDbContext<BomContext>(options =>
        options.UseSqlServer(settings.DbConnectionString));
}

static void ConfigureApplication(WebApplication app)
{
    app.MapEndpoints();
    app.MapGet("/", () => "Bom Service Running ....").WithTags("Healtz");

    using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>()
        .CreateScope();
    using var context = serviceScope.ServiceProvider.GetService<BomContext>();
    context!.Database.Migrate();
}