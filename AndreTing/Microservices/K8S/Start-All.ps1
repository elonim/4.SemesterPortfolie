$yamls = @(
    "platforms-depl.yaml",
    "commands-depl.yaml",
    "ingress-srv.yaml",
    "local-pvc.yaml",
    "mssql-plat-depl.yaml",
    "platforms-np-srv.yaml",
    "rabbitmq-depl.yaml"
)

foreach ($yaml in $yamls) {
    kubectl apply -f $yaml
}