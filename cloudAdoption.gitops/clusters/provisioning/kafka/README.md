# Connection to Kafka

## 1. From inside the cluster

Use the following configs to connect to kafka from a pod running in the cluster

```properties

bootstrap.servers: kafka-cluster-kafka-bootstrap.kafka.svc.cluster.local:9092
security.protocol: SASL_PLAINTEXT
sasl.mechanism: SCRAM-SHA-512
sasl.jaas.config: org.apache.kafka.common.security.scram.ScramLoginModule required username='<Kafka username>' password='<Kafka user password>';


```


## 2. From outside the cluster

Use the following configs to connect to kafka from a pod running in the cluster

```properties
bootstrap.servers=kafka-bootstrap.ttd-blue.azure.bmw.cloud:443
security.protocol=SASL_SSL
ssl.truststore.location=<path to truststore>
ssl.truststore.password=<truststore password>
sasl.mechanism: SCRAM-SHA-512
sasl.jaas.config: org.apache.kafka.common.security.scram.ScramLoginModule required username='<Kafka username>' password='<Kafka user password>';

```

### How to get kafka user credentials

```bash

## Get user name
kubectl get secret kafka-user -n kafka -o jsonpath='{.data.username}' | base64 -d > kafka-user.username

## Get user password
kubectl get secret kafka-user -n kafka -o jsonpath='{.data.password}' | base64 -d > kafka-user.password

## Get jaas config
kubectl get secret kafka-user -n kafka -o jsonpath='{.data.sasl\.jaas\.config}' | base64 -d > kafka-user.config

```

### How to create truststore

```bash

## Get Cluster Certs
kubectl get secret cluster-name-cluster-ca-cert -n kafka -o jsonpath='{.data.ca\.crt}' | base64 -d > ca.crt

## Create client truststore
keytool -import -trustcacerts -alias root -file ca.crt -keystore truststore.jks -storepass changeit -noprompt
```
