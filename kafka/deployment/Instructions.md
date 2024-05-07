# Commands

- Add the Bitnami Apache Kafka Helm Chart Repository
```bash
helm repo add bitnami https://charts.bitnami.com/bitnami
```

- Create a Namespace for Kafka
```bash
kubectl create namespace kafka
```

- Create the kafka-credentials secret
```bash
kubectl create secret generic kafka-credentials \
  --namespace kafka \
  --from-literal=kafka-user=test \
  --from-literal=kafka-password=test123
```

- Install Apache Kafka with KRaft using Helm and the values.yaml file
```bash
helm install kafka bitnami/kafka \
  --namespace kafka \
  --values values.yaml
```

- Check elements kafka:
```bash
kubectl get pods -n kafka
```

## options of values of this helm package:
https://github.com/bitnami/charts/blob/main/bitnami/kafka/values.yaml
