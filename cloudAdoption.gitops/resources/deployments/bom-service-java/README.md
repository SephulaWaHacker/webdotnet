# bom-service-java

This is a helm chart for bom-service-java

## Configuration

### Value files

The configurations and settings that are applied to an instance of the bom-service-java running on k8s are determined by combining the following values files

* The `values.yaml` file in the root directory. This contains the default configurations for all instances of bom-service-java

More info on how value files work can be found [here](https://helm.sh/docs/chart_template_guide/values_files/)

### Environment Variables

Environment variables are specified as map called `environmentVariables` in the values files mentioned above. The key in the map is the name of the environment variable, the value is the name of the environment variable.
The snippet below shows 2 environment variables `MQ_HOSTNAME` and `MQ_PORT` defined.
```yaml
environmentVariables:
  MQ_HOSTNAME: limq02vm.bmwgroup.net
  MQ_PORT: 1414
```

### Environment Secrets

Secrets are specified as map called `environmentSecrets` in the values files mentioned above. The key in the map is the name of the environment variable that will be populated with the K8s secret, the value is also a map with 2 keys. `secret_name` key is mandatory, it is the name of the secret in Azure KeyVault that contains the secret value. `key` is optional and defaults to the value of the `secret_name` when not specified, this is name of secret key in the K8s secret
The snippet below shows 2 environment variables `KAFKA_KEY` and `KAFKA_SECRET` defined from secrets.
```yaml
environmentSecrets:
  KAFKA_KEY:
    secret_name: kafka-key
  KAFKA_SECRET:
    secret_name: kafka-secret
    key: kafka-azure-secret
```

### Adding Secrets to Azure Key Vault

1. Login to az cli as a service principle using the following command

```shell
az login --service-principal --username "<your-service-principal-client-id>" --password "<your-service-principal-client-secret>" --tenant "<your-azure-tenant-id>"
```

2. Add secret to Azure KeyVault using the command below
```shell
az keyvault secret set --vault-name "<your-keyvault-name>" --name "<secret-name>" --value "<secret-value>"
```


### Parameters

Parameters that can be configured in the value files

| Parameter | Description | 
| --------- | ----------- | 
| `replicas` | Number of desired pods. Defaults to 1. |
| `revisionHistoryLimit` | The number of old ReplicaSets to retain to allow rollback.    |
| `metricsPath` | Path for prometheus metrics    |
| `servicePort` | The port on the container on which the service will be running on and used by the K8s service   |
| `serviceScheme` | Protocol used for the service  |
| `deploymentStrategy` | "Recreate" or "RollingUpdate"   |
| `terminationGracePeriodSeconds` | Optional duration in seconds the pod needs to terminate gracefully   |
| `ingress.enabled` | A flag to enable ingress   |
| `ingress.path` | The context path of the ingress   |
| `ingress.annotations` | Map of annotations for the ingress   |
| `autoscaling.enabled` | A flag to enable horizontal pod autoscaling   |
| `autoscaling.maxReplicas` | Maximum number of pods desired  |
| `autoscaling.targetCPUUtilizationPercentage` | target average CPU utilization for autoscaling (represented as a percentage of requested CPU)   |
| `livenessProbe.path` | Path used for checking for liveness  |
| `readinessProbe.path` | Path used for checking for readiness  |
| `resources.limits.cpu` | Max cpu required by bom-service-java  |
| `resources.limits.memory` | Max memory required by bom-service-java   |
| `resources.requests.cpu` | CPU required for startup   |
| `resources.requests.memory` | Memory required for startup   |
| `image.url` | The url to the docker image   |
| `image.tag` | The tag to the docker image   |
| `namespace` | Namespace to deploy the resource  |
| `environmentVariables` | Map of the environment variables required by bom-service-java. See the **Environment Variables** section above  |
| `environmentSecrets` | Map of the secrets provided as environment variables to bom-service-java. See the **Environment Secrets** section above   |



