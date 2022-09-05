### Parameters

Parameters that can be configured in the value files

| Parameter | Description | Default  |
| --------- | ----------- | -------- |
| `argocd.namespace` | Namespace to deploy argocd. | |
| `source.gitRepository` | The git repository containing the gitops manifests    | |
| `source.gitRevision` | The git branch/tag argocd will listen to for this plant and environment    | |
| `dynatraceTags` | a map of tags sent to dynatrace | |
| `environment` | The name of the deployment environment  | |
| `project.name` | The name of the argocd project for this argocd application  | |
| `plants` | A list of plants deployed by this app of apps| |
| `commonServices.enabled` | A flag for deploying or undeploying common services for this app of apps| |
| `monitoringServices.enabled` | A flag for deploying or undeploying monitoring services (prometheus, grafana) for this app of apps| |
| `deployed` | A flag for deploying or undeploying this app of apps. This can be used to remove a plant from an environment | true |
