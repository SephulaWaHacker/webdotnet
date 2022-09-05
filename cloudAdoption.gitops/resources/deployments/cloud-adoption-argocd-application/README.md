# cloud-adoption-argocd-application

This is an umbrella helm chart template responsible for defining ArgoCD applications for all the microservices that will be deployed in the cloud-adoption namespace for each environment.


## Configuration

Parameters that can be configured in the values files

| Parameter | Description  |
| --------- | ----------- |
| `applications` | map of applications deployed to the environment |

