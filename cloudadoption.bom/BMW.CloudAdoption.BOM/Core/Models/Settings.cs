using Confluent.Kafka;

namespace BMW.CloudAdoption.BOM.Core.Models;

public class Settings
{
    [ConfigurationKeyName("KAFKA_BOOTSTRAP_SERVER")]
    public string KafkaBootstrapServer { get; set; } = string.Empty;

    [ConfigurationKeyName("KAFKA_KEY")] public string? KafkaUsername { get; set; }

    [ConfigurationKeyName("KAFKA_SECRET")] public string? KafkaPassword { get; set; }
    
    [ConfigurationKeyName("KAFKA_CA_LOCATION")]
    public string KafkaCaLocation { get; set; } = string.Empty;

    [ConfigurationKeyName("KAFKA_PARTS_TOPIC")]
    public string KafkaPartsTopic { get; set; } = string.Empty;

    [ConfigurationKeyName("KAFKA_BOM_TOPIC")]
    public string KafkaBomTopic { get; set; } = string.Empty;

    [ConfigurationKeyName("KAFKA_SASLMECHANISM")]
    public SaslMechanism? KafkaSaslMechanism { get; set; }

    [ConfigurationKeyName("KAFKA_SSLENDPOINTIDENTIFICATIONALGORITHM")]
    public SslEndpointIdentificationAlgorithm? KafkaSslEndpointIdentificationAlgorithm { get; set; }
    [ConfigurationKeyName("KAFKA_SECURITYPROTOCOL")] public SecurityProtocol? KafkaSecurityProtocol { get; set; }
    

    [ConfigurationKeyName("DB_USER")] public string DbUser { get; set; } = string.Empty;

    [ConfigurationKeyName("DB_PASSWORD")] public string DbPassword { get; set; } = string.Empty;

    [ConfigurationKeyName("DB_HOST")] public string DbHost { get; set; } = string.Empty;

    [ConfigurationKeyName("DB_PORT")] public string DbPort { get; set; } = string.Empty;

    [ConfigurationKeyName("DB_NAME")] public string DbName { get; set; } = string.Empty;

    [ConfigurationKeyName("DB_TRUST_SERVER_CERTIFICATE")]
    public bool DbTrustServerCertificate { get; set; }

    public string DbConnectionString
        =>
            $"Server=tcp:{DbHost},{DbPort};Initial Catalog={DbName};Persist Security Info=False;User ID={DbUser};Password={DbPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate={DbTrustServerCertificate};Connection Timeout=30;";
}