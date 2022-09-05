# Connect to Hosted Kafka Instance form your local environment

1. Consumer and Broker settings for appsettings.Development.json

~~~
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "KAFKA_BOOTSTRAP_SERVER": "kafka-bootstrap.ttd-blue.azure.bmw.cloud:443",
  "KAFKA_KEY": "kafka-user",
  "KAFKA_SECRET": "Ul1tLy7USV9l",
  "KAFKA_PARTS_TOPIC": "bmw.cloudadoption.parts.v1",
  "KAFKA_BOM_TOPIC": "bmw.cloudadoption.bom.v1",
  "KAFKA_SASLMECHANISM": "ScramSha512",
  "KAFKA_SECURITYPROTOCOL": "SaslSsl",
  "KAFKA_CA_LOCATION": "<path_to_ca.crt>",
  "DB_USER": "mssql_admin_qMtWpM93",
  "DB_PASSWORD": "Y}YRZ$$hZlF]LhYA}BB@Eq([F",
  "DB_HOST": "dweuttdbluemssql.database.windows.net",
  "DB_PORT": "1433",
  "DB_NAME": "bom_csharp_development_emea"
}


~~~



